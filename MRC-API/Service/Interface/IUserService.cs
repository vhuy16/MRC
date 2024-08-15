using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response.User;

namespace MRC_API.Service.Interface
{
    public interface IUserService
    {
       Task<CreateNewAccountResponse> CreateNewAdminAccount(CreateNewAccountRequest createNewAccountRequest);
       Task<CreateNewAccountResponse> CreateNewManagerAccount(CreateNewAccountRequest createNewAccountRequest);
       Task<CreateNewAccountResponse> CreateNewCustomerAccount(CreateNewAccountRequest createNewAccountRequest);
       Task<LoginResponse> Login(Payload.Request.User.LoginRequest loginRequest);

    }
}
