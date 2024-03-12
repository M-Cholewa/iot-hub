using InfluxDB.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InfluxDB
{
    public class GeneralLog
    {
        [Column(IsTag = true)]
        public Guid DeviceId { get; set; }

        [Column(IsMeasurement = true)]
        public string Measurement { get; set; } = "GeneralLog";

        [Column("Message")]
        public string Message { get; set; } = string.Empty;

        [Column(IsTimestamp = true)]
        public DateTime DateUTC { get; set; }
    }
}
