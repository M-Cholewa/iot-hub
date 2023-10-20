using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RabbitMQ.RPC
{
    public class RpcResponse
    {
        public string? Message { get; set; }
        public string? CorelationId { get; set; }

        public RpcResponse(string? message, string? corelationId)
        {
            Message = message;
            CorelationId = corelationId;
        }

        public RpcResponse()
        {
        }
    }
}
