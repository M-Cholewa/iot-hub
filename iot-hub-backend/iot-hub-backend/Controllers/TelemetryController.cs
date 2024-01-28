using Business.InfluxRepository;
using Domain.Core;
using iot_hub_backend.Infrastructure.Security.AuthorizeAttribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iot_hub_backend.Controllers
{
    [Authorize]
    [HasRole(Role.User)]
    [Route("[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
        private readonly TelemetryRepository _telemetryRepository;

        public TelemetryController(TelemetryRepository telemetryRepository)
        {
            _telemetryRepository = telemetryRepository;
        }

        [HttpGet("All")]
        public List<Telemetry> GetTelemetries(Guid deviceId, string fieldName, DateTime sinceUTC, DateTime toUTC)
        {
            return _telemetryRepository.Get(deviceId, fieldName, sinceUTC, toUTC);
        }


        [HttpGet("FieldNames")]
        public async Task<List<string>> GetTelemetryFieldNames(Guid deviceId)
        {
            return await _telemetryRepository.GetFieldNames(deviceId);
        }
    }
}
