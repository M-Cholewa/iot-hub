using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.MQTT.Config
{
    public class MQTTConnectionConfig
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? ServerAddress { get; set; }
    }

}
