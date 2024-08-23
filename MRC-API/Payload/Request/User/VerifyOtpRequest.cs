namespace MRC_API.Payload.Request.User
{
    public class VerifyOtpRequest
    {
       public Guid UserId { get; set; }
       public string otpCheck {  get; set; }
    }
}
