namespace MRC_API.Utils
{
    public class OtpUltil
    {
        public static string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
