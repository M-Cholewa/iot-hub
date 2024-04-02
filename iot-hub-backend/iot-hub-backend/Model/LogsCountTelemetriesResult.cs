using Domain.InfluxDB;

namespace iot_hub_backend.Model
{
    public class LogsCountTelemetriesResult
    {
        public List<GeneralTelemetry> LogsInfo { get; set; } = new();
        public List<GeneralTelemetry> LogsWarning { get; set; } = new();
        public List<GeneralTelemetry> LogsError { get; set; } = new();
    }
}
