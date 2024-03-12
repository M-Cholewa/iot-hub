using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxDB.Client.Core;

namespace Domain.InfluxDB
{
    public class LogSeverity
    {
        public const string INFO = "Info";
        public const string WARNING = "Warning";
        public const string ERROR = "Error";
    }

    public class Log
    {
        [Column(IsMeasurement = true)]
        public string Measurement { get; set; } = "DeviceLog";

        [Column(IsTag = true)]
        public Guid DeviceId { get; set; }

        [Column("Severity", IsTag = true)]
        public string Severity { get; set; } = "";

        [Column("Message")]
        public string Message { get; set; } = "";

        [Column(IsTimestamp = true)]
        public DateTime DateUTC { get; set; }

    }
}
