using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MQTT
{
    public class LogTelemetry
    {
        public string Message { get; set; } = "";
        public string Severity { get; set; } = "";
    }
}
