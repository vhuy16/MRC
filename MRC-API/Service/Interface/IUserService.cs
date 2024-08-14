using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response.User;

namespace MRC_API.Service.Interface
{
    public interface IUserService
    {
       Task<CreateNewAccountResponse> CreateNewAccount(CreateNewAccountRequest createNewAccountRequest);

    }
}
