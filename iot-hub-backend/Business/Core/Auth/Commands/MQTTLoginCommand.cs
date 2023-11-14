using MediatR;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Auth.Commands
{
    public class MQTTLoginCommandResult
    {
        public MqttConnectReasonCode MqttConnectReasonCode { get; set; }
        public Domain.Core.MQTTUser? MQTTUser { get; set; }
    }

    public class MQTTLoginCommand : IRequest<MQTTLoginCommandResult>
    {
        public Guid ClientId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
