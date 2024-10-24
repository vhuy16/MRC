namespace MRC_API.Payload.Request.Email
{
    public class EmailSettings
    {
        // The email address from which the email is sent
        public string FromEmail { get; set; }

        // The SMTP host (e.g., smtp.gmail.com for Gmail)
        public string Host { get; set; }

        // The port number for the SMTP host (e.g., 587 for Gmail)
        public int Port { get; set; }

        // The username for authenticating with the SMTP server
        public string Username { get; set; }

        // The password for authenticating with the SMTP server
        public string Password { get; set; }
    }
}
