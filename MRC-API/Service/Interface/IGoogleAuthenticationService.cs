using MRC_API.Payload.Response.GoogleAuth;

namespace MRC_API.Service.Interface
{
    public interface IGoogleAuthenticationService
    {
        public Task<GoogleAuthResponse> AuthenticateGoogleUser(HttpContext context);

    }
}
