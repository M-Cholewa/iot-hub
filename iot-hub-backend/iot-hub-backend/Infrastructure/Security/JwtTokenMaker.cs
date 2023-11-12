using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iot_hub_backend.Infrastructure.Security
{
    public class JwtTokenMaker
    {
        public static string Make(Domain.Core.User user, JwtSettings jwtSettings)
        {
            var TokenSecret = jwtSettings.Key!;
            var TokenLifetime = TimeSpan.FromHours(8);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenSecret);

            var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Sub, user.Email!),
                    new(JwtRegisteredClaimNames.Email, user.Email !),
                    new(ClaimNames.UserId, user.Id.ToString()),
                };

            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    if (role.Key == null)
                    {
                        continue;
                    }

                    var claim = new Claim(role.Key, true.ToString(), ClaimValueTypes.Boolean);
                    claims.Add(claim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
