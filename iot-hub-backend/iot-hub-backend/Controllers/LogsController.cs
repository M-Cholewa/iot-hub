using Business.InfluxRepository;
using Domain.Core;
using Domain.InfluxDB;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
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

        public LogsController(LogRepository logRepository)
        {
            _logRepository = logRepository;
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
    }
}
