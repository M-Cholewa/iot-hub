using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RabbitMQ.RPC
{
    public class RpcRequest
    {
        public string ReplyTo { get; set; }
        public string MethodName { get; set; }
        public string ArgumentsJson { get; set; }
        public string CorelationId { get; set; }

        public RpcRequest(string methodName, string argumentsJson, string corelationId, string replyTo)
        {
            ReplyTo = replyTo;
            MethodName = methodName;
            ArgumentsJson = argumentsJson;
            CorelationId = corelationId;
        }

        public byte[] GetJsonBytes()
        {
            var jsonTxt = System.Text.Json.JsonSerializer.Serialize(this);
            return Encoding.UTF8.GetBytes(jsonTxt);
        }
    }
}
