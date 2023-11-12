using Business.Core.Device.Commands;
using Business.Core.User.Commands;
using Business.Infrastructure.Security;
using iot_hub_backend.Data;
using iot_hub_backend.Infrastructure.Security;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace iot_hub_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;
        private readonly IoTHubContext _context;
        //private readonly Business.Infrastructure.Security.IPasswordHasher _passHasher;
        private readonly JwtSettings _jwtSettings;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator, IoTHubContext context, IPasswordHasher passHasher, JwtSettings jwtSettings)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
            _passHasher = passHasher;
            _jwtSettings = jwtSettings;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [Authorize]
        [RequiresClaim(ClaimNames.UserRole, "true")]
        [HttpPost("ExecuteDirectMethod")]
        public async Task<ExecuteDirectMethodCommandResult> ExecuteDirectMethod([FromBody] ExecuteDirectMethodCommand cmd)
        {
            var res = await _mediator.Send(cmd).ConfigureAwait(false);

            return res;
        }

        //[HttpPost("token")]
        //public IActionResult GenerateToken([FromBody]TokenGenerationRequest request)
        //{
        //    var TokenSecret = _jwtSettings.Key!;
        //    var TokenLifetime = TimeSpan.FromHours(8);

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(TokenSecret);

        //    var claims = new List<Claim>
        //    {
        //        new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        //        new(JwtRegisteredClaimNames.Sub, request.Email),
        //        new(JwtRegisteredClaimNames.Email, request.Email),
        //        new("userid", request.Id.ToString()),
        //    };

        //    foreach(var claimPair in request.CustomClaims)
        //    {
        //        var claim = new Claim(claimPair.Key, claimPair.Value.ToString()!, ClaimValueTypes.Boolean);
        //        claims.Add(claim);
        //    }

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.UtcNow.Add(TokenLifetime),
        //        Issuer = _jwtSettings.Issuer,
        //        Audience = _jwtSettings.Audience,
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var jwt = tokenHandler.WriteToken(token);

        //    return Ok(jwt);
        //}

        [HttpPost("CreateUser")]
        public async Task<ActionResult<Domain.Core.User>> CreateUser([FromBody] AddUserCommand cmd)
        {
            string pwdHash = _passHasher.HashPassword(cmd.Password!);
            var user = new Domain.Core.User { Email = cmd.Email!, PasswordHash = pwdHash };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        //[HttpPost("Login")]
        //public ActionResult<string?> Login(string email, string password)
        //{
        //    var usr = _context.Users.Where(x => x.Email == email).First();

        //    if (usr == null)
        //    {
        //        // no user with this mail

        //        return StatusCode(StatusCodes.Status401Unauthorized, false);
        //    }

        //    var isPwdCorrect = _passHasher.VerifyHashedPassword(usr.PasswordHash!, password);

        //    if (isPwdCorrect)
        //    {
        //        return StatusCode(StatusCodes.Status200OK, true);
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status401Unauthorized, false);
        //    }

        //}

    }
}