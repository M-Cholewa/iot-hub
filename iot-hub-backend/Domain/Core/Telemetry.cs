using InfluxDB.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
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
