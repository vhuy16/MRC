namespace MRC_API.Configurations
{
    public class VNPaySettings
    {
        public string vnp_TmnCode { get; set; }         // Terminal code provided by VNPay
        public string vnp_HashSecret { get; set; }      // Secret key for hashing
        public string vnp_ReturnUrl { get; set; }       // URL to redirect after payment
        public string vnp_Url { get; set; }             // VNPay payment URL
    }
}
