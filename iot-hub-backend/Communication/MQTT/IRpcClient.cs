using Domain.MQTT;

namespace Communication.MQTT
{
    public interface IRpcClient
    {
        public Task<(RpcResult, RpcResponse?)> CallMethodAsync(string deviceId, string methodName, string argumentsJson, CancellationToken cancellationToken = default);
    }
}
