namespace MRC_API.Payload.Request.User
{
    public class VerifyAndResetPasswordRequest
    {
        public string NewPassword { get; set; }
        public string ComfirmPassword { get; set; }
    }
}
