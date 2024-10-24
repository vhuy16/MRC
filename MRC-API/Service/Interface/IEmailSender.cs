namespace MRC_API.Service.Interface
{
    public interface IEmailSender
    {
        Task SendVerificationEmailAsync(string email, string otp);
    }
}
