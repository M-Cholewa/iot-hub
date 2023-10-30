using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MQTT
{
    public class RpcResponse
    {
        public string? ResponseDataJson { get; set; }

        public RpcResponse(string? responseDataJson)
        {
            ResponseDataJson = responseDataJson;
        }

        public RpcResponse()
        {
        }
    }
}
