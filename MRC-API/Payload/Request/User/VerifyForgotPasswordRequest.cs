namespace MRC_API.Payload.Request.User
{
    public class VerifyForgotPasswordRequest
    {
        public Guid userId {  get; set; }
        public string otp { get; set; }
    }
}
