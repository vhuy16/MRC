using System.Security.Cryptography;
using System.Text;

namespace Bean_Mind.API.Utils
{
    public class PasswordUtil
    {
        public static string HashPassword(string rawPassword)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
            return Convert.ToBase64String(bytes);
        }
    }
}
