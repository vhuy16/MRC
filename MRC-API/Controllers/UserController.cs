using Azure;
using Bean_Mind.API.Utils;
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.User;
using MRC_API.Service.Interface;

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
        
    }
}
