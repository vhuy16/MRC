using AutoMapper;
using Business.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MRC_API.Payload.Response.GoogleAuth;
using MRC_API.Service.Interface;
using Repository.Entity;
using System.Security.Claims;

namespace MRC_API.Service.Implement
{
    public class GoogleAuthenticationService : BaseService<GoogleAuthenticationService>, IGoogleAuthenticationService
    {
        public GoogleAuthenticationService(IUnitOfWork<MrcContext> unitOfWork, ILogger<GoogleAuthenticationService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<GoogleAuthResponse> AuthenticateGoogleUser(HttpContext context)
        {
            var authenticateResult = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (authenticateResult.Principal == null) return null;
            var name = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);
            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            if (email == null) return null;
            var accessToken = authenticateResult.Properties.GetTokenValue("access_token");

            return new GoogleAuthResponse
            {
                FullName = name,
                Email = email,
                Token = accessToken
            };
        }
    }
}
