using Domain.Core;
using MediatR;
using MessageProcessing.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessing.Messages.Requests
{
    public class AddLogCommand : IRequest
    {
        public List<LogTelemetry>? Telemetry { get; set; }
        public Guid ClientId { get; set; }
    }
}
