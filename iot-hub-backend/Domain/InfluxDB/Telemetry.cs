using InfluxDB.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InfluxDB
{
    public class Telemetries
    {
        public static readonly string STATUS = "Status";
        public static readonly string STATUS_ONLINE = "Online";
        public static readonly string STATUS_OFFLINE = "Offline";
        public static readonly string FW_VERSION = "FirmwareVersion";
        public static readonly string UPTIME_S = "UptimeS";
        public static readonly string LAST_ACTIVITY = "LastActivity";
    }

    public class Telemetry
    {

        [Column(IsTag = true)]
        public Guid DeviceId { get; set; }

        [Column("FieldValue")]
        public object FieldValue { get; set; } = "";

        [Column("FieldName", IsMeasurement = true)]
        public string FieldName { get; set; } = "";

        [Column("FieldUnit")]
        public string FieldUnit { get; set; } = "";

        [Column(IsTimestamp = true)]
        public DateTime DateUTC { get; set; }
    }
}
