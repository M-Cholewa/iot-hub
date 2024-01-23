﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxDB.Client.Core;

namespace Domain.Core
{
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
