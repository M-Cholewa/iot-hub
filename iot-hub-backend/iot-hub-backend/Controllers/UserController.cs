using Business.Core.User.Commands;
using Business.Repository;
using Domain.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
using iot_hub_backend.Infrastructure.Extensions;
using Business.InfluxRepository;
using Domain.InfluxDB;
using iot_hub_backend.Model;

namespace iot_hub_backend.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserRepository _userRepository;
        private readonly GeneralTelemetryRepository _generalTelemetryRepository;

        public UserController(IMediator mediator, UserRepository userRepository, GeneralTelemetryRepository generalTelemetryRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _generalTelemetryRepository = generalTelemetryRepository;
        }

        [HttpPut]
        public async Task<RegisterUserCommandResult> RegisterUser([FromBody] RegisterUserCommand cmd)
        {
            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpDelete]
        [HasRole(Role.User)]
        public async Task<RemoveUserCommandResult> RemoveUser([FromBody] RemoveUserCommand cmd)
        {
            bool _canRemove = await User.IsAdminOrCertainUser(cmd.UserId, _userRepository);

            if (!_canRemove)
            {
                return new RemoveUserCommandResult { IsSuccess = false, Message = "Unauthorized" };
            }

            return await _mediator.Send(cmd).ConfigureAwait(false);
        }


        [HttpPatch("Email")]
        [HasRole(Role.User)]
        public async Task<UpdateUserEmailCommandResult> UpdateUserEmail([FromBody] UpdateUserEmailCommand cmd)
        {
            bool _canUpdate = await User.IsAdminOrCertainUser(cmd.UserId, _userRepository);

            if (!_canUpdate)
            {
                return new UpdateUserEmailCommandResult { IsSuccess = false, Message = "Unauthorized" };
            }

            return await _mediator.Send(cmd).ConfigureAwait(false);
        }


        [HttpPatch("Password")]
        [HasRole(Role.User)]
        public async Task<UpdateUserPasswordCommandResult> UpdateUserPassword([FromBody] UpdateUserPasswordCommand cmd)
        {
            bool _canUpdate = await User.IsAdminOrCertainUser(cmd.UserId, _userRepository);

            if (!_canUpdate)
            {
                return new UpdateUserPasswordCommandResult { IsSuccess = false, Message = "Unauthorized" };
            }

            return await _mediator.Send(cmd).ConfigureAwait(false);
        }

        [HttpGet("All")]
        [HasRole(Role.Admin)]
        public async Task<IList<User>?> GetAllUsers()
        {
            return await _userRepository.GetAllAsync().ConfigureAwait(false);
        }

        [HttpGet("DevicesOnlineCountTelemetries")]
        [HasRole(Role.User)]
        public async Task<List<GeneralTelemetry>> GetDevicesOnline(DateTime sinceUTC, DateTime toUTC)
        {
            var user = await User.GetUser(_userRepository);

            if (user == null)
            {
                return new List<GeneralTelemetry>();
            }

            return _generalTelemetryRepository.Get(user.Id, GeneralTelemetries.HOUR_DEVICES_ONLINE, sinceUTC, toUTC);
        }

        [HttpGet("MessagesLoggedCountTelemetries")]
        [HasRole(Role.User)]
        public async Task<List<GeneralTelemetry>> GetMessagesLogged(DateTime sinceUTC, DateTime toUTC)
        {
            var user = await User.GetUser(_userRepository);

            if (user == null)
            {
                return new List<GeneralTelemetry>();
            }

            return _generalTelemetryRepository.Get(user.Id, GeneralTelemetries.HOUR_LOGGED_MESSAGES, sinceUTC, toUTC);
        }

        [HttpGet("LogsCountTelemetries")]
        [HasRole(Role.User)]
        public async Task<LogsCountTelemetriesResult> GetLogsCount(DateTime sinceUTC, DateTime toUTC)
        {
            var user = await User.GetUser(_userRepository);

            if (user == null)
            {
                return new LogsCountTelemetriesResult();
            }

            var infos = _generalTelemetryRepository.Get(user.Id, GeneralTelemetries.HOUR_INFOS, sinceUTC, toUTC);
            var warnings = _generalTelemetryRepository.Get(user.Id, GeneralTelemetries.HOUR_WARNINGS, sinceUTC, toUTC);
            var errors = _generalTelemetryRepository.Get(user.Id, GeneralTelemetries.HOUR_ERRORS, sinceUTC, toUTC);

            return new LogsCountTelemetriesResult { LogsError = errors, LogsInfo = infos, LogsWarning = warnings };
        }

        [HttpGet("SummedMessageCount")]
        [HasRole(Role.User)]
        public async Task<int> GetSummedMessageCount()
        {
            var user = await User.GetUser(_userRepository);

            if (user == null)
            {
                return 0;
            }

            var summedMsgCount = _generalTelemetryRepository.GetLatest(user.Id, GeneralTelemetries.SUMMED_MESSAGE_COUNT);

            int summedMsgCountInt = 0;

            try
            {
                summedMsgCountInt = Convert.ToInt32(summedMsgCount?.FieldValue);
            }
            catch { }


            return summedMsgCountInt;
        }


        [HttpGet("UserDeviceCount")]
        public async Task<int> GetDeviceCount()
        {
            var user = await User.GetUser(_userRepository);

            if (user == null)
            {
                return 0;
            }

            return user.Devices?.Count ?? 0;
        }
    }
}
