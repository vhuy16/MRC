using Azure;
using Bean_Mind.API.Utils;
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.User;
using MRC_API.Service.Interface;
using Repository.Paginate;

namespace MRC_API.Controllers
{
    [ApiController]
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService) : base(logger)
        {
            _userService = userService;
        }
        [HttpPost(ApiEndPointConstant.User.RegisterAdmin)]
        [ProducesResponseType(typeof(CreateNewAccountResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNewAccount([FromBody] CreateNewAccountRequest createNewAccountRequest)
        {
            CreateNewAccountResponse createNewAccountResponse = await _userService.CreateNewAdminAccount(createNewAccountRequest);
            if (createNewAccountResponse == null)
            {
                return Problem(MessageConstant.UserMessage.CreateUserAdminFail);
            }
            return CreatedAtAction(nameof(CreateNewAccount), createNewAccountResponse);
        }

        [HttpPost(ApiEndPointConstant.User.RegisterManager)]
        [ProducesResponseType(typeof(CreateNewAccountResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> CreateNewMangerAccount([FromBody] CreateNewAccountRequest createNewAccountRequest)
        {
            CreateNewAccountResponse createNewAccountResponse = await _userService.CreateNewManagerAccount(createNewAccountRequest);
            if (createNewAccountResponse == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = "Failed to create teacher",
                    TimeStamp = TimeUtils.GetCurrentSEATime()
                });
            }
            return Ok(createNewAccountResponse);
        }
        [HttpPost(ApiEndPointConstant.User.RegisterCustomer)]
        [ProducesResponseType(typeof(CreateNewAccountResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNewCustomerAccount([FromBody] CreateNewAccountRequest createNewAccountRequest)
        {
            CreateNewAccountResponse createNewAccountResponse = await _userService.CreateNewCustomerAccount(createNewAccountRequest);
            if (createNewAccountResponse == null)
            {
                return Problem(MessageConstant.UserMessage.CreateUserAdminFail);
            }
            return CreatedAtAction(nameof(CreateNewAccount), createNewAccountResponse);
        }

        [HttpPost(ApiEndPointConstant.User.Login)]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var loginResponse = await _userService.Login(loginRequest);
            if (loginResponse == null) {

                return Unauthorized(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = MessageConstant.LoginMessage.InvalidUsernameOrPassword,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(loginResponse);
        }

        [HttpDelete(ApiEndPointConstant.User.DeleteUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var response = await _userService.DeleteUser(id);
            return Ok(response);
        }

        [HttpGet(ApiEndPointConstant.User.GetAllUser)]
        [ProducesResponseType(typeof(IPaginate<GetUserResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllCategory([FromQuery] int? page, [FromQuery] int? size)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _userService.GetAllUser(pageNumber, pageSize);
            if (response == null)
            {
                return Problem(MessageConstant.UserMessage.UserIsEmpty);
            }
            return Ok(response);
        }

        [HttpGet(ApiEndPointConstant.User.GetUser)]
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var response = await _userService.GetUser(id);
            return Ok(response);
        }

        [HttpPut(ApiEndPointConstant.User.UpdateUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateUserRequest updateUserRequest)
        {
            var response = await _userService.UpdateUser(id, updateUserRequest);
            return Ok(response);
        }
        [HttpPost(ApiEndPointConstant.User.VerifyOtp)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest verifyOtpRequest)
        {
            // Gọi phương thức dịch vụ để xác thực OTP
            bool isOtpValid = await _userService.VerifyOtp(verifyOtpRequest.UserId, verifyOtpRequest.otpCheck);

            return Ok(isOtpValid);
        }
    }
}
