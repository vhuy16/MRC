using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MRC_API.Payload.Response;

namespace MRC_API.Infrastructure
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public CustomAuthorizeAttribute(string roles)
        {
            _roles = roles.Split(',').Select(r => r.Trim()).ToArray();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new JsonResult(new ApiResponse()
                {
                    status = "401",
                    message = "Bạn chưa đăng nhập",
                    data = null
                })
                {
                    StatusCode = 401
                };
            }
            else if (!_roles.Any(role => user.IsInRole(role)))
            {
                context.Result = new JsonResult(new ApiResponse()
                {
                    status = "403",
                    message = "Bạn không có quyền",
                    data = null
                })
                {
                    StatusCode = 403
                };
            }
        }
    }
}
