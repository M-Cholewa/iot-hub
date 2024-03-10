using InfluxDB.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InfluxDB
{
    public class ConsoleRecord
    {
        [Column(IsTag = true)]
        public Guid DeviceId { get; set; }

        [Column(IsMeasurement = true)]
        public string Measurement { get; set; } = "Console";

        [Column("RpcResult", IsTag = true)]
        public string RpcResult { get; set; } = "";

        [Column("ResponseDataJson")]
        public string ResponseDataJson { get; set; } = "";

        [Column(IsTimestamp = true)]
        public DateTime DateUTC { get; set; }
    }
}
