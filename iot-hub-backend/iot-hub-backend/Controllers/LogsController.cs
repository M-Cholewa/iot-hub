using Business.InfluxRepository;
using Business.Repository;
using Domain.Core;
using Domain.InfluxDB;
using iot_hub_backend.Infrastructure.Extensions;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
using iot_hub_backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iot_hub_backend.Controllers
{
    [Authorize]
    [HasRole(Role.User)]
    [Route("[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {

        private readonly LogRepository _logRepository;
        private readonly GeneralLogRepository _generalLogRepository;
        private readonly UserRepository _userRepository;

        public LogsController(LogRepository logRepository, GeneralLogRepository generalLogRepository, UserRepository userRepository)
        {
            _logRepository = logRepository;
            _generalLogRepository = generalLogRepository;
            _userRepository = userRepository;
        }

        [HttpGet("All")]
        public List<Log> GetAllLogs(Guid deviceId)
        {
            return _logRepository.GetAll(deviceId);
        }

        [HttpGet("LastN")]
        public List<Log> GetLastLogs(Guid deviceId, int limit)
        {
            return _logRepository.GetAll(deviceId, limit);
        }

        [HttpGet("AllGeneralLogs")]
        public List<GeneralLog> GetAllGeneralLogs(Guid deviceId)
        {
            return _generalLogRepository.GetAll(deviceId).OrderBy(x=>x.DateUTC).ToList();
        }

        [HttpGet("UserDeviceLogsLastN")]
        public async Task<List<UserDeviceLogsLastNResult>> UserDeviceLogsLastN(int limit)
        {
            var user = await User.GetUser(_userRepository);

            if (user == null || user.Devices == null)
            {
                return new List<UserDeviceLogsLastNResult>();
            }

            var lastLogs = new List<Log>();

            foreach (var device in user.Devices)
            {
                lastLogs.AddRange(_logRepository.GetAll(device.Id, 3));
            }

            lastLogs.Sort((x, y) => x.DateUTC.CompareTo(y.DateUTC));

            lastLogs = lastLogs.Take(limit).ToList();

            var userLastDeviceLogs = lastLogs.Select(x => new UserDeviceLogsLastNResult { DeviceName = user.Devices.FirstOrDefault(d => d.Id == x.DeviceId)?.Name ?? "?", Log = x });

            return userLastDeviceLogs.ToList();
        }

    }
}
