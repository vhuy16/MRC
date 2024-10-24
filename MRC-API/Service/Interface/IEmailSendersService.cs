namespace MRC_API.Service.Interface
{
    public interface IEmailSendersService
    {
        Task SendVerificationEmailAsync(string email, string otp);
    }
}
