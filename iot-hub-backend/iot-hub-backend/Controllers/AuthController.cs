using Business.Core.Auth.Commands;
using iot_hub_backend.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iot_hub_backend.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IMediator mediator, JwtSettings jwtSettings)
        {
            _mediator = mediator;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string?>> Login([FromBody] LoginCommand cmd)
        {
            var _login = await _mediator.Send(cmd).ConfigureAwait(false);

            if (!_login.IsSuccess || _login.User == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "");
            }

            // make token

            var TokenSecret = _jwtSettings.Key!;
            var TokenLifetime = TimeSpan.FromHours(8);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenSecret);

            var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Sub, _login.User.Email!),
                    new(JwtRegisteredClaimNames.Email, _login.User.Email !),
                    new(ClaimNames.UserId, _login.User.Id.ToString()),
                };

            if (_login.User.Roles != null)
            {
                foreach (var role in _login.User.Roles)
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
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return StatusCode(StatusCodes.Status200OK, jwt);
        }
    }
}
