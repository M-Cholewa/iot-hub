using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business.Core.Device.Commands
{
    public class CreateDeviceCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public Domain.Core.Device? ResultDevice { get; set; }
        public string? MqttApiKey { get; set; }
    }

    public class CreateDeviceCommand : IRequest<CreateDeviceCommandResult>
    {
        public Domain.Core.Device? Device { get; set; }
        public string? MqttUsername { get; set; } = "";
        public string? MqttPassword { get; set; } = "";
        public Guid OwnerId { get; set; }
    }
}
