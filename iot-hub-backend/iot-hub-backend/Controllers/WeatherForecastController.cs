using Business.Core.Device.Commands;
using Business.Core.User.Commands;
using Business.Infrastructure.Security;
using Domain.Core;
using iot_hub_backend.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        private readonly Business.Infrastructure.Security.IPasswordHasher _passHasher;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator, IoTHubContext context, IPasswordHasher passHasher)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
            _passHasher = passHasher;
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


        [HttpPost("ExecuteDirectMethod")]
        public async Task<ExecuteDirectMethodCommandResult> ExecuteDirectMethod([FromBody] ExecuteDirectMethodCommand cmd)
        {
            var res = await _mediator.Send(cmd).ConfigureAwait(false);

            return res;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<Domain.Core.User>> CreateUser([FromBody] AddUserCommand cmd)
        {
            string pwdHash = _passHasher.HashPassword(cmd.Password!);
            var user = new Domain.Core.User { Email = cmd.Email!, PasswordHash = pwdHash };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

    }
}