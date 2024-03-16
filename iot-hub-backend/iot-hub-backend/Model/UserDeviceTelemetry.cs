using Domain.Core;

namespace iot_hub_backend.Model
{
    public class UserDeviceTelemetry
    {
        public Device? Device { get; set; }
        public DeviceTelemetry? DeviceTelemetry { get; set; }
    }
}
