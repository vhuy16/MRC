using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Service.Implement;
using MRC_API.Service.Interface;
using MRC_API.Payload.Response;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MRC_API.Controllers

{
    [ApiController]
    [Route(ApiEndPointConstant.GoogleAuthentication.GoogleAuthenticationEndpoint)]
    public class GoogleAuthenticationController : BaseController<GoogleAuthenticationController>
    {
        private readonly IUserService _userService;
        private readonly IGoogleAuthenticationService _googleAuthenticationService;

        public GoogleAuthenticationController(ILogger<GoogleAuthenticationController> logger, IUserService userService, IGoogleAuthenticationService googleAuthenticationService) : base(logger)
        {
            _userService = userService;
            _googleAuthenticationService = googleAuthenticationService;
        }


        [HttpGet(ApiEndPointConstant.GoogleAuthentication.GoogleLogin)]
        public IActionResult Login()
        {
            //var props = new AuthenticationProperties { RedirectUri = $"https://mrc.vn/auth/callback" };
            var props = new AuthenticationProperties { RedirectUri = $"http://localhost:5173" };
            //var props = new AuthenticationProperties { RedirectUri = $"api/v1/google-auth/signin-google/" };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }


        [HttpGet(ApiEndPointConstant.GoogleAuthentication.GoogleSignIn)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignInAndSignUpByGoogle()
        {
            var googleAuthResponse = await _googleAuthenticationService.AuthenticateGoogleUser(HttpContext);
            var checkAccount = await _userService.GetAccountByEmail(googleAuthResponse.Email);
            if (!checkAccount)
            {
                var response = await _userService.CreateNewUserAccountByGoogle(googleAuthResponse);
                if (response == null)
                {
                    _logger.LogError($"Create new user account failed with account");
                    return Problem(MessageConstant.UserMessage.CreateUserAdminFail);
                }
            }
            var token = await _userService.CreateTokenByEmail(googleAuthResponse.Email);
            googleAuthResponse.Token = token;
            return Ok(new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Login successful",
                data = googleAuthResponse
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
         
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Logout successful",
                data = null
            });
        }
    } 
}
