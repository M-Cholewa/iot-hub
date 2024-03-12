using Domain.MQTT;
using MQTTnet.Server;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Schema;
using Domain.InfluxDB;

namespace MQTTServer.Infrastructure
{
    public static class MqttServerExtensions
    {

        public static async Task SendStatusTelemetry(this MqttServer mqttServer, string clientId, string statusValue)
        {
            var teles = new List<BaseTelemetry>
            {
                new() { FieldName = "Status", Unit = "", Value = statusValue }
            };

            var telesJson = JsonSerializer.Serialize(teles);
            var telemAppMsg = new MqttApplicationMessage
            {
                Topic = $"telemetry_queue/{clientId}/telemetry",
                PayloadSegment = Encoding.ASCII.GetBytes(telesJson)
            };

            var telemInjAppMsg = new InjectedMqttApplicationMessage(telemAppMsg) { SenderClientId = clientId };
            await mqttServer.InjectApplicationMessage(telemInjAppMsg);
        }

        public static async Task SendStatusLog(this MqttServer mqttServer, string clientId, string statusMessage)
        {
            var logs = new List<LogTelemetry>
            {
                new () {Message = statusMessage, Severity = LogSeverity.INFO}
            };

            var logsJson = JsonSerializer.Serialize(logs);
            var logAppMsg = new MqttApplicationMessage
            {
                Topic = $"telemetry_queue/{clientId}/log",
                PayloadSegment = Encoding.ASCII.GetBytes(logsJson)
            };

            var injLogAppMsg = new InjectedMqttApplicationMessage(logAppMsg) { SenderClientId = clientId };
            await mqttServer.InjectApplicationMessage(injLogAppMsg);
        }
    }
}
