using AutoMapper;
using Azure.Core;
using Bean_Mind.API.Utils;
using Business.Interface;
using Microsoft.AspNetCore.Identity.Data;
using MRC_API.Constant;
using MRC_API.Payload.Request.Category;
using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response.GoogleAuth;
using MRC_API.Payload.Response.User;
using MRC_API.Service.Interface;
using MRC_API.Utils;
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
        public UserService(IUnitOfWork<MrcContext> unitOfWork, ILogger<UserService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateNewAccountResponse> CreateNewAdminAccount(CreateNewAccountRequest createNewAccountRequest)
        {
            _logger.LogInformation($"create new user with {createNewAccountRequest.UserName}");
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(createNewAccountRequest.Email, emailPattern))
            {
                throw new BadHttpRequestException(MessageConstant.PatternMessage.EmailIncorrect);
            }
            string phonePattern = @"^0\d{9}$";
            if (!Regex.IsMatch(createNewAccountRequest.PhoneNumber, phonePattern))
            {
                throw new BadHttpRequestException(MessageConstant.PatternMessage.PhoneIncorrect);
            }
            User newUser = new User()
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
            bool isSuccesfully = await _unitOfWork.CommitAsync() > 0;
            CreateNewAccountResponse createNewAccountResponse = null;
            if(isSuccesfully)
            {
                createNewAccountResponse = new CreateNewAccountResponse()
                {
                    Username = newUser.UserName,
                    Password = newUser.Password,
                };
            }
            return createNewAccountResponse;
        }

        public async Task<CreateNewAccountResponse> CreateNewManagerAccount(CreateNewAccountRequest createNewAccountRequest)
        {
            _logger.LogInformation($"create new user with {createNewAccountRequest.UserName}");

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(createNewAccountRequest.Email, emailPattern))
            {
                throw new BadHttpRequestException(MessageConstant.PatternMessage.EmailIncorrect);
            }
            string phonePattern = @"^0\d{9}$";
            if (!Regex.IsMatch(createNewAccountRequest.PhoneNumber, phonePattern))
            {
                throw new BadHttpRequestException(MessageConstant.PatternMessage.PhoneIncorrect);
            }
            var manager = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: m => m.UserName.Equals(createNewAccountRequest.UserName));

            if (manager != null)
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.AccountExisted);
            }
            var managerPhone = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: m => m.PhoneNumber.Equals(createNewAccountRequest.PhoneNumber));

            if (managerPhone != null)
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.PhoneExisted);
            }
            var managerEmail = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: m => m.Email.Equals(createNewAccountRequest.Email));

            if (managerEmail != null)
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.EmailExisted);
            }
            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = createNewAccountRequest.UserName,
                Password = PasswordUtil.HashPassword(createNewAccountRequest.Password),
                FullName = createNewAccountRequest.FullName,
                Email = createNewAccountRequest.Email,
                Status = StatusEnum.Available.GetDescriptionFromEnum(),
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                //Status = true,
                PhoneNumber = createNewAccountRequest.PhoneNumber,
                Gender = createNewAccountRequest.Gender.GetDescriptionFromEnum().ToString(),
                Role = RoleEnum.Manager.GetDescriptionFromEnum()

            };
            await _unitOfWork.GetRepository<User>().InsertAsync(newUser);
            bool isSuccesfully = await _unitOfWork.CommitAsync() > 0;
            GenderEnum gender = EnumUtil.ParseEnum<GenderEnum>(newUser.Gender);
            CreateNewAccountResponse createNewAccountResponse = null;
            if (isSuccesfully)
            {
                createNewAccountResponse = new CreateNewAccountResponse()
                {
                    Username = newUser.UserName,
                    Password = newUser.Password,
                    Email = newUser.Email,
                    FullName = newUser.FullName,
                    Gender = gender,
                    PhoneNumber = newUser.PhoneNumber
                };
            }
            return createNewAccountResponse;
        }
        public async Task<CreateNewAccountResponse> CreateNewCustomerAccount(CreateNewAccountRequest createNewAccountRequest)
        {
            _logger.LogInformation($"create new user with {createNewAccountRequest.UserName}");

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(createNewAccountRequest.Email, emailPattern))
            {
                throw new BadHttpRequestException(MessageConstant.PatternMessage.EmailIncorrect);
            }
            string phonePattern = @"^0\d{9}$";
            if (!Regex.IsMatch(createNewAccountRequest.PhoneNumber, phonePattern))
            {
                throw new BadHttpRequestException(MessageConstant.PatternMessage.PhoneIncorrect);
            }
            var manager = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: m => m.UserName.Equals(createNewAccountRequest.UserName));

            if (manager != null)
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.AccountExisted);
            }
            var managerPhone = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: m => m.PhoneNumber.Equals(createNewAccountRequest.PhoneNumber));

            if (managerPhone != null)
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.PhoneExisted);
            }
            var managerEmail = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: m => m.Email.Equals(createNewAccountRequest.Email));

            if (managerEmail != null)
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.EmailExisted);
            }
            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = createNewAccountRequest.UserName,
                Password = PasswordUtil.HashPassword(createNewAccountRequest.Password),
                FullName = createNewAccountRequest.FullName,
                Email = createNewAccountRequest.Email,
                Status = StatusEnum.Available.GetDescriptionFromEnum(),
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Role = RoleEnum.Customer.GetDescriptionFromEnum(),
                PhoneNumber = createNewAccountRequest.PhoneNumber,
                Gender = createNewAccountRequest.Gender.GetDescriptionFromEnum().ToString()
            };
            await _unitOfWork.GetRepository<User>().InsertAsync(newUser);
            bool isSuccesfully = await _unitOfWork.CommitAsync() > 0;
            GenderEnum gender = EnumUtil.ParseEnum<GenderEnum>(newUser.Gender);
            CreateNewAccountResponse createNewAccountResponse = null;
            if (isSuccesfully)
            {
                createNewAccountResponse = new CreateNewAccountResponse()
                {
                    Username = newUser.UserName,
                    Password = newUser.Password,
                    Email = newUser.Email,
                    FullName = newUser.FullName,
                    Gender = gender,
                    PhoneNumber = newUser.PhoneNumber
                };
            }
            return createNewAccountResponse;
        }
        public async Task<LoginResponse> Login(Payload.Request.User.LoginRequest loginRequest)
        {
            Expression<Func<User, bool>> searchFilter = p =>
                  p.UserName.Equals(loginRequest.Username) &&
                  p.Password.Equals(PasswordUtil.HashPassword(loginRequest.Password)) &&
                  (p.Role == RoleEnum.Manager.GetDescriptionFromEnum() ||
                   p.Role == RoleEnum.Admin.GetDescriptionFromEnum() ||
                   p.Role == RoleEnum.Customer.GetDescriptionFromEnum()) &&
                   p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum());
            
            User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: searchFilter);

            if (user == null)
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.AccountNotExist);
            };

            RoleEnum role = EnumUtil.ParseEnum<RoleEnum>(user.Role);
            Tuple<String, Guid> guildClaim = new Tuple<string, Guid>("userID", user.Id);
            LoginResponse loginResponse = new LoginResponse()
            {
                RoleEnum = role.ToString(),
                UserId = user.Id,
                UserName = user.UserName,
            };
            var token = JwtUtil.GenerateJwtToken(user, guildClaim);
            loginResponse.token = token;
            return loginResponse;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(id) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if (user == null)
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.UserNotExist);
            }
            user.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
            user.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<User>().UpdateAsync(user);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<IPaginate<GetUserResponse>> GetAllUser(int page, int size)
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
            return users;
        }

        public async Task<GetUserResponse> GetUser(Guid id)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                selector: u => new GetUserResponse()
                {
                    UserId = u.Id,
                    Email = u.Email,
                    FullName= u.FullName,
                    PhoneNumber = u.PhoneNumber,
                    Gender = u.Gender
                },
                predicate: u => u.Id.Equals(id) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if (user == null)
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.UserNotExist);
            }
            return user;
        }

        public async Task<bool> UpdateUser(Guid id, UpdateUserRequest updateUserRequest)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(id) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if (user == null)
            {
                throw new BadHttpRequestException(MessageConstant.UserMessage.UserNotExist);
            }
            user.FullName = string.IsNullOrEmpty(updateUserRequest.FullName) ? user.FullName : updateUserRequest.FullName;
            user.Email = string.IsNullOrEmpty(updateUserRequest.Email) ? user.Email : updateUserRequest.Email;
            user.PhoneNumber = string.IsNullOrEmpty(updateUserRequest.PhoneNumber) ? user.PhoneNumber : updateUserRequest.PhoneNumber;
            user.Gender = updateUserRequest.Gender.HasValue ? updateUserRequest.Gender.GetDescriptionFromEnum() : user.Gender.GetDescriptionFromEnum();
            user.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<User>().UpdateAsync(user);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
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

        public async Task<CreateNewAccountResponse> CreateNewUserAccountByGoogle(GoogleAuthResponse request)
        {

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
            CreateNewAccountResponse response = null;
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful)
            {

                response = new CreateNewAccountResponse()
                {
                    Email = newUser.Email, 
                    FullName = newUser.FullName
                };
            }
            return response;
        }
    }
}
