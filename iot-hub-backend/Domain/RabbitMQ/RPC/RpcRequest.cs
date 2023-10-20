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
        public string Message { get; set; }
        public string CorelationId { get; set; }

        public RpcRequest(string replyTo, string message, string corelationId)
        {
            ReplyTo = replyTo;
            Message = message;
            CorelationId = corelationId;
        }

        public byte[] GetJsonBytes()
        {
            var jsonTxt = System.Text.Json.JsonSerializer.Serialize(this);
            return Encoding.UTF8.GetBytes(jsonTxt);
        }
    }
}
