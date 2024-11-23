using AutoMapper;
using Azure.Core;
using Bean_Mind.API.Utils;
using Business.Interface;
using Microsoft.AspNetCore.Identity.Data;
using MimeKit;
using MRC_API.Constant;
using MRC_API.Payload.Request.Category;
using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.GoogleAuth;
using MRC_API.Payload.Response.User;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Newtonsoft.Json;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;


namespace MRC_API.Service.Implement
{
    public class UserService : BaseService<UserService>, IUserService
    {
        private readonly IEmailSender _emailSender;

        public UserService(IUnitOfWork<MrcContext> unitOfWork, ILogger<UserService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {  

            _emailSender = emailSender;
        }

        public async Task<ApiResponse> CreateNewAdminAccount(CreateNewAccountRequest createNewAccountRequest)
        {
            _logger.LogInformation($"Creating new admin account for {createNewAccountRequest.UserName}");

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(createNewAccountRequest.Email, emailPattern))
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.PatternMessage.EmailIncorrect,
                    data = null
                };
            }

            string phonePattern = @"^0\d{9}$";
            if (!Regex.IsMatch(createNewAccountRequest.PhoneNumber, phonePattern))
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.PatternMessage.PhoneIncorrect,
                    data = null
                };
            }

            User newUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = createNewAccountRequest.UserName,
                Password = PasswordUtil.HashPassword(createNewAccountRequest.Password),
                Status = StatusEnum.Available.GetDescriptionFromEnum(),
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Gender = createNewAccountRequest.Gender.GetDescriptionFromEnum().ToString(),
                PhoneNumber = createNewAccountRequest.PhoneNumber,
                Role = RoleEnum.Admin.GetDescriptionFromEnum()
            };

            await _unitOfWork.GetRepository<User>().InsertAsync(newUser);
            bool isSuccessfully = await _unitOfWork.CommitAsync() > 0;

            if (isSuccessfully)
            {
                var createNewAccountResponse = new CreateNewAccountResponse
                {
                    Username = newUser.UserName
                };

                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Admin account created successfully",
                    data = createNewAccountResponse
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status500InternalServerError.ToString(),
                message = "Failed to create admin account",
                data = null
            };
        }

        public async Task<ApiResponse> CreateNewManagerAccount(CreateNewAccountRequest createNewAccountRequest)
        {
            _logger.LogInformation($"Creating new manager account for {createNewAccountRequest.UserName}");

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(createNewAccountRequest.Email, emailPattern))
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.PatternMessage.EmailIncorrect,
                    data = null
                };
            }

            string phonePattern = @"^0\d{9}$";
            if (!Regex.IsMatch(createNewAccountRequest.PhoneNumber, phonePattern))
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.PatternMessage.PhoneIncorrect,
                    data = null
                };
            }

            var manager = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: m => m.UserName.Equals(createNewAccountRequest.UserName));

            if (manager != null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.UserMessage.AccountExisted,
                    data = null
                };
            }

            User newUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = createNewAccountRequest.UserName,
                Password = PasswordUtil.HashPassword(createNewAccountRequest.Password),
                FullName = createNewAccountRequest.FullName,
                Email = createNewAccountRequest.Email,
                Status = StatusEnum.Available.GetDescriptionFromEnum(),
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                PhoneNumber = createNewAccountRequest.PhoneNumber,
                Gender = createNewAccountRequest.Gender.GetDescriptionFromEnum().ToString(),
                Role = RoleEnum.Manager.GetDescriptionFromEnum()
            };

            await _unitOfWork.GetRepository<User>().InsertAsync(newUser);
            bool isSuccessfully = await _unitOfWork.CommitAsync() > 0;

            if (isSuccessfully)
            {
                var createNewAccountResponse = new CreateNewAccountResponse
                {
                    Username = newUser.UserName,
                    Email = newUser.Email,
                    FullName = newUser.FullName,
                    Gender = EnumUtil.ParseEnum<GenderEnum>(newUser.Gender),
                    PhoneNumber = newUser.PhoneNumber
                };

                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Manager account created successfully",
                    data = createNewAccountResponse
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status500InternalServerError.ToString(),
                message = "Failed to create manager account",
                data = null
            };
        }

        public async Task<bool> VerifyOtp (Guid UserId, string otpCheck)
        {
            var otp = await _unitOfWork.GetRepository<Otp>().SingleOrDefaultAsync(predicate: p => p.OtpCode.Equals(otpCheck) && p.UserId.Equals(UserId));
            if(otp != null && TimeUtils.GetCurrentSEATime() < otp.ExpiresAt && otp.IsValid == true)
            {
                var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.Id.Equals(UserId));
                if(user != null)
                {
                    user.Status = StatusEnum.Available.GetDescriptionFromEnum();
                    _unitOfWork.GetRepository<User>().UpdateAsync(user);
                    _unitOfWork.GetRepository<Otp>().DeleteAsync(otp); // Delete the OTP record
                
                    
                    await _unitOfWork.CommitAsync();
                    return true;
                }

            }
            return false;
        }

        public async Task<ApiResponse> CreateNewCustomerAccount(CreateNewAccountRequest createNewAccountRequest)
        {
            _logger.LogInformation($"Creating new customer account for {createNewAccountRequest.UserName}");

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(createNewAccountRequest.Email, emailPattern))
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.PatternMessage.EmailIncorrect,
                    data = null
                };
            }

            string phonePattern = @"^0\d{9}$";
            if (!Regex.IsMatch(createNewAccountRequest.PhoneNumber, phonePattern))
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.PatternMessage.PhoneIncorrect,
                    data = null
                };
            }

            var customer = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: m => m.UserName.Equals(createNewAccountRequest.UserName));

            if (customer != null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.UserMessage.AccountExisted,
                    data = null
                };
            }

            User newUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = createNewAccountRequest.UserName,
                Password = PasswordUtil.HashPassword(createNewAccountRequest.Password),
                FullName = createNewAccountRequest.FullName,
                Email = createNewAccountRequest.Email,
                Status = StatusEnum.Unavailable.GetDescriptionFromEnum(),
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Role = RoleEnum.Customer.GetDescriptionFromEnum(),
                PhoneNumber = createNewAccountRequest.PhoneNumber,
                Gender = createNewAccountRequest.Gender.GetDescriptionFromEnum().ToString()
            };

            await _unitOfWork.GetRepository<User>().InsertAsync(newUser);

            Cart newCart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = newUser.Id,
                Status = StatusEnum.Available.GetDescriptionFromEnum(),
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime()
            };
            await _unitOfWork.GetRepository<Cart>().InsertAsync(newCart);

            bool isSuccessfully = await _unitOfWork.CommitAsync() > 0;

            if (isSuccessfully)
            {
                var createNewAccountResponse = new CreateNewAccountResponse
                {
                    Id = newUser.Id,
                    Username = newUser.UserName,
                    Email = newUser.Email,
                    FullName = newUser.FullName,
                    Gender = EnumUtil.ParseEnum<GenderEnum>(newUser.Gender),
                    PhoneNumber = newUser.PhoneNumber
                };
                string otp = OtpUltil.GenerateOtp();
                var otpRecord = new Otp
                {
                    Id = Guid.NewGuid(),
                    UserId = newUser.Id,
                    OtpCode = otp,
                    CreateDate = TimeUtils.GetCurrentSEATime(),
                    ExpiresAt = TimeUtils.GetCurrentSEATime().AddMinutes(10),
                    IsValid = true
                };
                await _unitOfWork.GetRepository<Otp>().InsertAsync(otpRecord);
                await _unitOfWork.CommitAsync();

                // Send OTP email
                await SendOtpEmail(newUser.Email, otp);

                // Optionally, handle OTP expiration as discussed
                ScheduleOtpCancellation(otpRecord.Id, TimeSpan.FromMinutes(10));
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Customer account created successfully",
                    data = createNewAccountResponse
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status500InternalServerError.ToString(),
                message = "Failed to create customer account",
                data = null
            };
        }
        public async Task<ApiResponse> Login(Payload.Request.User.LoginRequest loginRequest)
        {
            // Define the search filter for the user
            Expression<Func<User, bool>> searchFilter = p =>
                  p.UserName.Equals(loginRequest.Username) &&
                  p.Password.Equals(PasswordUtil.HashPassword(loginRequest.Password)) &&
                  (p.Role == RoleEnum.Manager.GetDescriptionFromEnum() ||
                   p.Role == RoleEnum.Admin.GetDescriptionFromEnum()) &&
                  (!p.DelDate.HasValue);

            // Retrieve the user based on the search filter
            User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: searchFilter);
            if(user == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status401Unauthorized.ToString(),
                    message = MessageConstant.LoginMessage.InvalidUsernameOrPassword,
                    data = null
                };
            }

            if (user.Status.Equals(StatusEnum.Unavailable.GetDescriptionFromEnum()))
            {
                string otp = OtpUltil.GenerateOtp();
                var otpRecord = new Otp
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    OtpCode = otp,
                    CreateDate = TimeUtils.GetCurrentSEATime(),
                    ExpiresAt = TimeUtils.GetCurrentSEATime().AddMinutes(10),
                    IsValid = true
                };
                await _unitOfWork.GetRepository<Otp>().InsertAsync(otpRecord);
                await _unitOfWork.CommitAsync();

                // Send OTP email
                await SendOtpEmail(user.Email, otp);

                // Optionally, handle OTP expiration as discussed
                ScheduleOtpCancellation(otpRecord.Id, TimeSpan.FromMinutes(10));
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    WarnMessage = "You need to verify your email, we have sent you an OTP",
                    data = new GetUserResponse() 
                    { 
                        UserId = user.Id,
                        Email = user.Email,
                        FullName = user.FullName,
                        Gender = user.Gender,
                        PhoneNumber = user.PhoneNumber
                    }
                };
            }

            // Create the login response
            RoleEnum role = EnumUtil.ParseEnum<RoleEnum>(user.Role);
            Tuple<string, Guid> guildClaim = new Tuple<string, Guid>("userID", user.Id);
            var token = JwtUtil.GenerateJwtToken(user, guildClaim);

            // Create the login response object
            var loginResponse = new LoginResponse()
            {
                RoleEnum = role.ToString(),
                UserId = user.Id,
                UserName = user.UserName,
                token = token // Assign the generated token
            };

            // Return a success response
            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Login successful.",
                data = loginResponse
            };
        }


        public async Task<ApiResponse> LoginCustomer(Payload.Request.User.LoginRequest loginRequest)
        {
            // Define the search filter for the user
            Expression<Func<User, bool>> searchFilter = p =>
                  p.UserName.Equals(loginRequest.Username) &&
                  p.Password.Equals(PasswordUtil.HashPassword(loginRequest.Password)) &&
                  (p.Role == RoleEnum.Customer.GetDescriptionFromEnum()) &&(!p.DelDate.HasValue);

            // Retrieve the user based on the search filter
            User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: searchFilter);
            if (user == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status401Unauthorized.ToString(),
                    message = MessageConstant.LoginMessage.InvalidUsernameOrPassword,
                    data = null
                };
            }

            if (user.Status.Equals(StatusEnum.Unavailable.GetDescriptionFromEnum()))
            {
                string otp = OtpUltil.GenerateOtp();
                var otpRecord = new Otp
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    OtpCode = otp,
                    CreateDate = TimeUtils.GetCurrentSEATime(),
                    ExpiresAt = TimeUtils.GetCurrentSEATime().AddMinutes(10),
                    IsValid = true
                };
                await _unitOfWork.GetRepository<Otp>().InsertAsync(otpRecord);
                await _unitOfWork.CommitAsync();

                // Send OTP email
                await SendOtpEmail(user.Email, otp);

                // Optionally, handle OTP expiration as discussed
                ScheduleOtpCancellation(otpRecord.Id, TimeSpan.FromMinutes(10));
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    WarnMessage = "You need to verify your email, we have sent you an OTP",
                    data = new GetUserResponse()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        FullName = user.FullName,
                        Gender = user.Gender,
                        PhoneNumber = user.PhoneNumber
                    }
                };
            }

            // Create the login response
            RoleEnum role = EnumUtil.ParseEnum<RoleEnum>(user.Role);
            Tuple<string, Guid> guildClaim = new Tuple<string, Guid>("userID", user.Id);
            var token = JwtUtil.GenerateJwtToken(user, guildClaim);

            // Create the login response object
            var loginResponse = new LoginResponse()
            {
                RoleEnum = role.ToString(),
                UserId = user.Id,
                UserName = user.UserName,
                token = token // Assign the generated token
            };

            // Return a success response
            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Login successful.",
                data = loginResponse
            };
        }
        public async Task<ApiResponse> DeleteUser(Guid id)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(id) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.UserMessage.UserNotExist,
                    data = null
                };
            }

            var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(
                predicate: c => c.UserId.Equals(user.Id));

            if (cart != null)
            {
                var cartItems = await _unitOfWork.GetRepository<CartItem>().GetListAsync(predicate: ci => ci.CartId.Equals(cart.Id));
                foreach (var cartItem in cartItems)
                {
                     _unitOfWork.GetRepository<CartItem>().DeleteAsync(cartItem);
                }

                cart.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
                cart.UpDate = TimeUtils.GetCurrentSEATime();
                _unitOfWork.GetRepository<Cart>().UpdateAsync(cart);
            }

            user.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
            user.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<User>().UpdateAsync(user);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "User deleted successfully.",
                    data = null
                };
            }
            else
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to delete user.",
                    data = null
                };
            }
        }

        public async Task<ApiResponse> GetAllUser(int page, int size)
        {
            var users = await _unitOfWork.GetRepository<User>().GetPagingListAsync(
                selector: u => new GetUserResponse()
                {
                    UserId = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Gender = u.Gender,
                },
                predicate: u => u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()),
                page: page,
                size: size);

            int totalItems = users.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if(users == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Users retrieved successfully.",
                    data = new LinkedList<CartItem>()
                };
            }
            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Users retrieved successfully.",
                data = users
            };
        }


        public async Task<ApiResponse> GetUser(Guid id)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                selector: u => new GetUserResponse()
                {
                    UserId = u.Id,
                    Email = u.Email,
                    FullName = u.FullName,
                    PhoneNumber = u.PhoneNumber,
                    Gender = u.Gender
                },
                predicate: u => u.Id.Equals(id) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.UserMessage.UserNotExist,
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "User retrieved successfully.",
                data = user
            };
        }

        public async Task<ApiResponse> UpdateUser(Guid id, UpdateUserRequest updateUserRequest)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(id) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.UserMessage.UserNotExist,
                    data = null
                };
            }

            user.FullName = string.IsNullOrEmpty(updateUserRequest.FullName) ? user.FullName : updateUserRequest.FullName;
            user.Email = string.IsNullOrEmpty(updateUserRequest.Email) ? user.Email : updateUserRequest.Email;
            user.PhoneNumber = string.IsNullOrEmpty(updateUserRequest.PhoneNumber) ? user.PhoneNumber : updateUserRequest.PhoneNumber;
            user.Gender = updateUserRequest.Gender.HasValue ? updateUserRequest.Gender.GetDescriptionFromEnum() : user.Gender.GetDescriptionFromEnum();
            user.UpDate = TimeUtils.GetCurrentSEATime();

            _unitOfWork.GetRepository<User>().UpdateAsync(user);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "User updated successfully.",
                    data = user
                };
            }
            else
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to update the user.",
                    data = null
                };
            }
        }


        public async Task<string> CreateTokenByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Username cannot be null or empty", nameof(email));
            }
            var account = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: p => p.Email.Equals(email)
            );
            if (account == null) throw new BadHttpRequestException("Account not found");
            var guidClaim = new Tuple<string, Guid>("userId", account.Id);
            var token = JwtUtil.GenerateJwtToken(account, guidClaim);
            // _logger.LogInformation($"Token: {token} ");
            return token;
        }

        public async Task<bool> GetAccountByEmail(string email)
        {
            if (email == null) throw new BadHttpRequestException("Email cannot be null");

            var account = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: p => p.Email.Equals(email)
            );
            return account != null;
        }
        public async Task<ApiResponse> CreateNewUserAccountByGoogle(GoogleAuthResponse request)
        {
            // Check if the user already exists
            var existingUser = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Email.Equals(request.Email) &&
                                u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (existingUser != null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = "User account already exists.",
                    data = new CreateNewAccountResponse
                    {
                        Email = existingUser.Email,
                        FullName = existingUser.FullName
                    }
                };
            }

            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                FullName = request.FullName,
                UserName = request.Email.Split("@")[0],
                Role = RoleEnum.Customer.GetDescriptionFromEnum(),
                Status = StatusEnum.Available.GetDescriptionFromEnum(),
                Password = PasswordUtil.HashPassword("12345678"),
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime()
            };

            await _unitOfWork.GetRepository<User>().InsertAsync(newUser);

            Cart newCart = new Cart()
            {
                Id = Guid.NewGuid(),
                UserId = newUser.Id,
                Status = StatusEnum.Available.GetDescriptionFromEnum(),
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime()
            };

            await _unitOfWork.GetRepository<Cart>().InsertAsync(newCart);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status201Created.ToString(),
                    message = "User account created successfully.",
                    data = new CreateNewAccountResponse
                    {
                        Email = newUser.Email,
                        FullName = newUser.FullName
                    }
                };
            }
            else
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to create user account.",
                    data = null
                };
            }
        }

        private async Task ScheduleOtpCancellation(Guid otpId, TimeSpan delay)
        {
            await Task.Delay(delay);

            var otpRepository = _unitOfWork.GetRepository<Otp>();
            var otp = await otpRepository.SingleOrDefaultAsync(predicate: p => p.Id.Equals(otpId));

            if (otp != null && otp.IsValid && TimeUtils.GetCurrentSEATime() >= otp.ExpiresAt)
            {
                otp.IsValid = false;
                otpRepository.UpdateAsync(otp);
                await _unitOfWork.CommitAsync();
            }
        }
        private async Task SendOtpEmail(string email, string otp)
        {
            try
            {
                // Create a new MimeMessage instance
                var message = new MimeMessage();

                // Set the sender's email address
                message.From.Add(new MailboxAddress("MRC", "mrc.web@outlook.com"));

                // Set the recipient's email address
                message.To.Add(new MailboxAddress("", email));

                // Set the subject of the email
                message.Subject = "Welcome to MRC";

                // Create a body builder to construct the email body
                var bodyBuilder = new BodyBuilder();

                // Set the HTML content of the email, including the OTP (without unnecessary bold tags)
                bodyBuilder.HtmlBody = $"Your OTP code is: {otp}. This code is valid for 10 minutes.";

                // Set the body of the message
                message.Body = bodyBuilder.ToMessageBody();

                // Send the email using the _emailSender service (assuming it's properly configured)
                await _emailSender.SendVerificationEmailAsync(email, otp);
            }
            catch (Exception ex)
            {
                // Handle email sending errors (optional)
                Console.WriteLine($"Error sending OTP email: {ex.Message}");
            }
        }

        public async Task<bool> ForgotPassword(Payload.Request.User.ForgotPasswordRequest request)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(request.Email, emailPattern))
            {
                throw new BadHttpRequestException(MessageConstant.PatternMessage.EmailIncorrect);
            }

            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Email.Equals(request.Email) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null) 
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.AccountNotExist);
            }

            string otp = OtpUltil.GenerateOtp();
            var otpRecord = new Otp
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                OtpCode = otp,
                CreateDate = TimeUtils.GetCurrentSEATime(),
                ExpiresAt = TimeUtils.GetCurrentSEATime().AddMinutes(10),
                IsValid = true
            };
            await _unitOfWork.GetRepository<Otp>().InsertAsync(otpRecord);
            await SendOtpEmail(user.Email, otp);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            ScheduleOtpCancellation(otpRecord.Id, TimeSpan.FromMinutes(10));
            return isSuccessful;

        }

        public async Task<bool> VerifyAndResetPassword(Guid id,VerifyAndResetPasswordRequest request)
        {
            var otp = await _unitOfWork.GetRepository<Otp>().SingleOrDefaultAsync(
                predicate: o => o.OtpCode.Equals(request.Otp) && o.UserId.Equals(id));
            if (otp == null)
            {
                throw new BadHttpRequestException("OTP không chính xác");
            }

            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(id) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if (user == null)
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.UserNotExist);
            }

            if (!request.NewPassword.Equals(request.ComfirmPassword))
            {
                throw new BadHttpRequestException("Mật khẩu xác nhận không trùng");
            }

            user.Password = PasswordUtil.HashPassword(request.NewPassword);
            user.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<User>().UpdateAsync(user);

            _unitOfWork.GetRepository<Otp>().DeleteAsync(otp);
            await _unitOfWork.CommitAsync();
            return true;

        }

        public async Task<ApiResponse> GetUser()
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);

            if (userId == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = "User ID not found.",
                    data = null
                };
            }

            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                selector: u => new GetUserResponse()
                {
                    UserId = u.Id,
                    Email = u.Email,
                    FullName = u.FullName,
                    PhoneNumber = u.PhoneNumber,
                    Gender = u.Gender,
                    Role = u.Role,
                },
                predicate: u => u.Id.Equals(userId.Value) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = "User not found.",
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "User retrieved successfully.",
                data = user
            };
        }

    }
}
