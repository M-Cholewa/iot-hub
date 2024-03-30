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
        [Column("Method")]
        public string Method { get; set; } = "";
        [Column("Payload")]
        public string Payload { get; set; } = "";

        [Column("RpcResult")]
        public string RpcResult { get; set; } = "";

        [Column("ResponseDataJson")]
        public string ResponseDataJson { get; set; } = "";

        [Column(IsTimestamp = true)]
        public DateTime DateUTC { get; set; }
    }
}
