﻿using Domain.Core;
using MediatR;
using MessageProcessing.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessing.Messages.Requests
{
    public class AddTelemetryCommand : IRequest
    {
        public List<BaseTelemetry>? Telemetry { get; set; }
        public Guid ClientId { get; set; }
    }
}
