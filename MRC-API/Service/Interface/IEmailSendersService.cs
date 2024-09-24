namespace MRC_API.Service.Interface
{
    public interface IEmailSendersService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
