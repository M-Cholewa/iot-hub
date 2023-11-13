using Business.Core.Auth.Commands;
using Business.Core.User.Commands;
using Business.Repository;
using iot_hub_backend.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace iot_hub_backend.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly JwtSettings _jwtSettings;

        public UserController(IMediator mediator, JwtSettings jwtSettings)
        {
            _mediator = mediator;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("Register")]
        public async Task<RegisterUserCommandResult> RegisterUser([FromBody] RegisterUserCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginCommand cmd)
        {
            var _login = await _mediator.Send(cmd).ConfigureAwait(false);

            if (!_login.IsSuccess || _login.User == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "");
            }

            var token = JwtTokenMaker.Make(_login.User, _jwtSettings);

            return StatusCode(StatusCodes.Status200OK, token);
        }
    }
}
