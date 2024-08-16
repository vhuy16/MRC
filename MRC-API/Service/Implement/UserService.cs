using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using Microsoft.AspNetCore.Identity.Data;
using MRC_API.Constant;
using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response.User;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;
using System.Linq.Expressions;
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
            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = createNewAccountRequest.UserName,
                Password = PasswordUtil.HashPassword(createNewAccountRequest.Password),
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Gender = createNewAccountRequest.Gender.GetDescriptionFromEnum().ToString(),
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
            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = createNewAccountRequest.UserName,
                Password = PasswordUtil.HashPassword(createNewAccountRequest.Password),
                FullName = createNewAccountRequest.FullName,
                Email = createNewAccountRequest.Email,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                //Status = true,
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
                    Gender = gender
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
            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = createNewAccountRequest.UserName,
                Password = PasswordUtil.HashPassword(createNewAccountRequest.Password),
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Role = RoleEnum.Customer.GetDescriptionFromEnum()

            };
            await _unitOfWork.GetRepository<User>().InsertAsync(newUser);
            bool isSuccesfully = await _unitOfWork.CommitAsync() > 0;
            CreateNewAccountResponse createNewAccountResponse = null;
            if (isSuccesfully)
            {
                createNewAccountResponse = new CreateNewAccountResponse()
                {
                    Username = newUser.UserName,
                    Password = newUser.Password,
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
                   p.Role == RoleEnum.Customer.GetDescriptionFromEnum());
            
            User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: searchFilter);

            if (user == null) return null;

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

    }
}
