using MQTTnet.Extensions.Rpc;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.MQTT.Extensions
{
    public static class MqttRpcClientExtensions
    {

        /// <summary>
        /// Execute RPC on specific device
        /// The device should subscribe to MQTTnet.RPC/+/{clientId}.{methodName}
        /// </summary>
        /// <param name="client"></param>
        /// <param name="timeout"></param>
        /// <param name="methodName"></param>
        /// <param name="payload"></param>
        /// <param name="clientId">Id of device executing the method</param>
        /// <param name="qualityOfServiceLevel"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Task<byte[]> ExecuteAsync(this IMqttRpcClient client, TimeSpan timeout, string clientId, string methodName, string payload, MqttQualityOfServiceLevel qualityOfServiceLevel, IDictionary<string, object>? parameters = null)
        {
            var newMethodName = clientId + "." + methodName;
            return client.ExecuteAsync(timeout, newMethodName, payload, qualityOfServiceLevel, parameters);
        }


    }
}
