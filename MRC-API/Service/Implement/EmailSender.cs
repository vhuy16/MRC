using Microsoft.Extensions.Options;
using MRC_API.Payload.Request.Email;
using MRC_API.Service.Interface;
using System.Net.Mail;
using System.Net;

public class EmailSender : IEmailSendersService
{
    private readonly string _emailAddress;
    private readonly string _appPassword;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailAddress = emailSettings.Value.EmailAddress;
        _appPassword = emailSettings.Value.AppPassword;
    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(_emailAddress, _appPassword)
        };

        return client.SendMailAsync(
            new MailMessage(from: _emailAddress,
                             to: email,
                             subject,
                             message)
        );
    }
}
