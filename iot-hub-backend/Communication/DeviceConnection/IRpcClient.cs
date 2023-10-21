using Domain.RabbitMQ.RPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DeviceConnection
{
    public interface IRpcClient : IDisposable
    {
        public Task<(RpcResult, RpcResponse?)> CallMethodAsync(string deviceId, string methodName, string argumentsJson, CancellationToken cancellationToken = default);
    }
}
