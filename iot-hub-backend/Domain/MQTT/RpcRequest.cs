using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MQTT
{
    public class RpcRequest
    {
        public string ArgumentsJson { get; set; }

        public RpcRequest(string argumentsJson)
        {
            ArgumentsJson = argumentsJson;
        }

        public string GetJsonString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
