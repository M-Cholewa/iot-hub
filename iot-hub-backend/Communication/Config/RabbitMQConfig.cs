using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Config
{
    public class RabbitMQConfig
    {
        public static readonly int RPC_TIMEOUT_MS = 3500;
        public static readonly string RPC_REPLY_QUEUE_NAME = "rpc_reply_queue";
        public static readonly string MQTT_EXCHANGE = "amq.topic";
    }
}
