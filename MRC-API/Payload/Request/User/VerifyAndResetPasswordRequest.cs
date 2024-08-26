namespace MRC_API.Payload.Request.User
{
    public class VerifyAndResetPasswordRequest
    {
        public string Otp {  get; set; }
        public string NewPassword { get; set; }
        public string ComfirmPassword { get; set; }
    }
}
