using library_management_system.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace library_management_system.Services
{
    public class StudentTokenService
    {
        public static ClientToken GenerateToken(Users user)
        {
            string Secret = "Studentsecret";
            string Audience = "AudienceClientStudent";
            string Issuer = "IssuerClientStudent";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var Credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var Expiration = DateTime.UtcNow.AddHours(2);
            var Claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            JwtSecurityToken token = new JwtSecurityToken(
                audience: Audience,
                issuer: Issuer,
                claims: Claims,
                expires: Expiration,
                signingCredentials: Credentials
            );
            ClientToken clientToken = new ClientToken();
            clientToken.Token = new JwtSecurityTokenHandler().WriteToken(token);
            clientToken.DateExpiration = Expiration;
            return clientToken;

        }
    }
}
