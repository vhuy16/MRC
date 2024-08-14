
using Microsoft.IdentityModel.Tokens;
using Repository.Entity;
using Repository.Enum;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MRC_API.Utils
{
    public class JwtUtil
    {
        private JwtUtil()
        {
        }

        public static string GenerateJwtToken(User account, Tuple<string, Guid> guidClaim)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            // Tạo một mảng byte để lưu trữ khóa có đủ kích thước
            byte[] keyBytes = new byte[32]; // 32 byte = 256 bit
            using (var rng = new RNGCryptoServiceProvider())
            {
                // Điền mảng byte với dữ liệu ngẫu nhiên từ RNGCryptoServiceProvider
                rng.GetBytes(keyBytes);
            }

            SymmetricSecurityKey secrectKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f"));
            var credentials = new SigningCredentials(secrectKey, SecurityAlgorithms.HmacSha256Signature);
            List<Claim> claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, account.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, account.UserName.ToString()),
            new Claim(ClaimTypes.Role, account.Role),
            new Claim(ClaimTypes.Name, account.UserName)
        };
            if (guidClaim != null) claims.Add(new Claim(guidClaim.Item1, guidClaim.Item2.ToString()));
            var expires = account.Role.Equals(RoleEnum.Customer.GetDescriptionFromEnum())
                ? DateTime.Now.AddDays(15)
                : DateTime.Now.AddDays(30);
            var token = new JwtSecurityToken("BeanMindSystem", null, claims, notBefore: DateTime.Now, expires, credentials);
            return jwtHandler.WriteToken(token);
        }
    }
}
