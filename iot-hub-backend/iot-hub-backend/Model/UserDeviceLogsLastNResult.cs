
using Domain.InfluxDB;

namespace iot_hub_backend.Model
{
    public class UserDeviceLogsLastNResult
    {
        public string DeviceName { get; set; } = string.Empty;
        public Log Log { get; set; } = new Log();
    }
}
