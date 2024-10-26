using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Service.Implement;
using MRC_API.Service.Interface;

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
            var props = new AuthenticationProperties { RedirectUri = $"http://localhost:5173/auth/callback" };
            //var props = new AuthenticationProperties { RedirectUri = $"api/v1/google-auth/signin-google/" };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }


        [HttpGet(ApiEndPointConstant.GoogleAuthentication.GoogleSignIn)]
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
            return Ok(googleAuthResponse);
        }
    } 
}
