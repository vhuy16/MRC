using Google.Apis.Auth.OAuth2;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MRC_API.Service.Interface;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using MRC_API.Payload.Request.Email;


public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendVerificationEmailAsync(string email, string otp)
    {
        // Create a new email message
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("MRC", _emailSettings.FromEmail));
        message.To.Add(new MailboxAddress(string.Empty, email)); // Use string.Empty for the name if you don't have one
        message.Subject = "OTP Verification";
        
        // Set the body of the email
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $@"
    <div style='font-family: Arial, sans-serif; color: #333;'>
        <h2>Your OTP Code</h2>
        <p>Dear User,</p>
        <p>Thank you for using our service! To complete your verification process, please use the following OTP (One-Time Password) code:</p>
        <h1 style='color: #2E86C1;'>{otp}</h1>
        <p>This code is valid for the next 10 minutes. Please do not share this code with anyone.</p>

        <h3>What happens next?</h3>
        <p>Once you've entered the OTP code, you'll be able to complete the verification process and access your account.</p>

        <p>If you did not request this code, please disregard this message. If you keep receiving OTPs without making any requests, we recommend updating your account security.</p>

        <p>Thank you for choosing our service!</p>
        
        <p>Best regards,<br>Your Company Name Support Team</p>
        
        <p style='font-size: 12px; color: #888;'>If you have any questions, feel free to <a href='mailto:support@yourcompany.com'>contact our support team</a>.</p>
    </div>";
        message.Body = bodyBuilder.ToMessageBody();

        // Send the email using your preferred SMTP server
        using (var client = new SmtpClient())
        {
            try
            {
                // Disable SSL certificate validation for local testing (not recommended for production)
                client.ServerCertificateValidationCallback = (s, c, chain, errors) => true;

                // Connect to the SMTP server
                await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);

                // Authenticate using the provided credentials
                await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);

                // Send the email
                await client.SendAsync(message);

                // Disconnect cleanly
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new InvalidOperationException("Error sending email", ex);
            }
        }
    }
}
