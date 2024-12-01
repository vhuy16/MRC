using Microsoft.AspNetCore.Mvc;
using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.GoogleAuth;
using MRC_API.Payload.Response.User;
using Repository.Paginate;

namespace MRC_API.Service.Interface
{
    public interface IUserService
    {
       Task<ApiResponse> CreateNewAdminAccount(CreateNewAccountRequest createNewAccountRequest);
       Task<ApiResponse> CreateNewManagerAccount(CreateNewAccountRequest createNewAccountRequest);
       Task<ApiResponse> CreateNewCustomerAccount(CreateNewAccountRequest createNewAccountRequest);
       Task<ApiResponse> Login(LoginRequest loginRequest);
       Task<ApiResponse> LoginCustomer(LoginRequest loginRequest);
       Task<ApiResponse> DeleteUser(Guid id);
       Task<ApiResponse> GetAllUser(int page, int size);
       Task<ApiResponse> GetUser(Guid id);
       Task<ApiResponse> GetUser();
       Task<ApiResponse> UpdateUser(Guid id, UpdateUserRequest updateUserRequest);
       Task<string> CreateTokenByEmail(string email);

       Task<bool> GetAccountByEmail(string email);
       Task<bool> VerifyOtp(Guid UserId, string otpCheck);
       Task<ApiResponse> CreateNewUserAccountByGoogle(GoogleAuthResponse response);
       Task<ApiResponse> ForgotPassword(ForgotPasswordRequest request);
       Task<ApiResponse> ResetPassword(VerifyAndResetPasswordRequest request);
       Task<ApiResponse> VerifyForgotPassword(Guid userId, string otp);
    }
}
