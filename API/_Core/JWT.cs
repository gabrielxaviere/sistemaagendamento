using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Utils
{
    public class JWT
    {
        private const string secretKey = "SuaChaveSecretaDeTeste";
        private const string issuer = "SeuEmissorDeTeste";
        private const string audience = "SuaAudienciaDeTeste";


        public string Create(int id, int expirationMinutes = 60)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = key
            };

            try
            {
                return tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ValidateToken(string token)
        {
            var principal = DecodeToken(token);

            return principal != null;
        }

        public int GetUserByToken(string token)
        {
            var decodedPrincipal = DecodeToken(token);

            if (decodedPrincipal != null)
            {
                if (int.TryParse(decodedPrincipal?.Identity?.Name, out int userId))
                {
                    return userId;
                }
            }

            return -1;
        }
    }
}
