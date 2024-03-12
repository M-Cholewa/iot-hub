using InfluxDB.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InfluxDB
{
    public class GeneralTelemetries
    {
        public static readonly string SUMMED_MESSAGE_COUNT = "SummedMessageCount";
        public static readonly string HOUR_INFOS = "HourInfos";
        public static readonly string HOUR_WARNINGS = "HourWarnings";
        public static readonly string HOUR_ERRORS = "HourErrors";
        public static readonly string HOUR_DEVICES_ONLINE = "HourDevicesOnline";
        public static readonly string HOUR_LOGGED_MESSAGES = "HourLoggedMessages";

    }
    public class GeneralTelemetry
    {
        [Column(IsTag = true)]
        public Guid UserId { get; set; }

        [Column("FieldName", IsMeasurement = true)]
        public string FieldName { get; set; } = "";

        [Column("FieldValue")]
        public object FieldValue { get; set; } = "";

        [Column(IsTimestamp = true)]
        public DateTime DateUTC { get; set; }
    }
}
