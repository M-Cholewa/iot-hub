using Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Device.Commands
{
    public class GetDeviceTelemetryCommandResult
    {
        public DeviceTelemetry? DeviceTelemetry { get; set; }
    }

    public class GetDeviceTelemetryCommand : IRequest<GetDeviceTelemetryCommandResult>
    {
        public Guid DeviceId { get; set; }

        public GetDeviceTelemetryCommand()
        {
        }
    }
}
