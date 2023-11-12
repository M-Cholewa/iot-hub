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
            _jwtSettings = jwtSettings;
        }


        //[Authorize]
        //[RequiresClaim(ClaimNames.UserRole, "true")]
        //[HttpPost("ExecuteDirectMethod")]
        //public async Task<ExecuteDirectMethodCommandResult> ExecuteDirectMethod([FromBody] ExecuteDirectMethodCommand cmd)
        //{
        //    var res = await _mediator.Send(cmd).ConfigureAwait(false);

        //    return res;
        //}

        //[HttpPost("CreateUser")]
        //public async Task<ActionResult<Domain.Core.User>> CreateUser([FromBody] RegisterUserCommand cmd)
        //{
        //    string pwdHash = _passHasher.HashPassword(cmd.Password!);
        //    var user = new Domain.Core.User { Email = cmd.Email!, PasswordHash = pwdHash };

        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return Ok(user);
        //}


    }
}