using Domain.Core;

namespace Domain.Core
{
    public class DeviceTelemetry
    {
        public string FirmwareVersion { get; set; } = "Unknown";
        public uint UptimeS { get; set; } = 0;
        public string Status { get; set; } = "Unknown";
        public DateTime LastActivityUTC { get; set; }
    }
}
