using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response.GoogleAuth;
using MRC_API.Payload.Response.User;
using Repository.Paginate;

namespace MRC_API.Service.Interface
{
    public interface IUserService
    {
       Task<CreateNewAccountResponse> CreateNewAdminAccount(CreateNewAccountRequest createNewAccountRequest);
       Task<CreateNewAccountResponse> CreateNewManagerAccount(CreateNewAccountRequest createNewAccountRequest);
       Task<CreateNewAccountResponse> CreateNewCustomerAccount(CreateNewAccountRequest createNewAccountRequest);
       Task<LoginResponse> Login(LoginRequest loginRequest);
       Task<bool> DeleteUser(Guid id);
       Task<IPaginate<GetUserResponse>> GetAllUser(int page, int size);
       Task<GetUserResponse> GetUser(Guid id);
       Task<bool> UpdateUser(Guid id, UpdateUserRequest updateUserRequest);
       Task<string> CreateTokenByEmail(string email);

       Task<bool> GetAccountByEmail(string email);
       Task<bool> VerifyOtp(Guid UserId, string otpCheck);
       Task<CreateNewAccountResponse> CreateNewUserAccountByGoogle(GoogleAuthResponse response);
       Task<bool> ForgotPassword(ForgotPasswordRequest request);
       Task<bool> VerifyAndResetPassword(Guid id,VerifyAndResetPasswordRequest request);
    }
}
