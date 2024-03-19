using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessing.Messages.Commands
{
    public class CreateGeneralTelemetriesCommand : IRequest
    {
        public DateTime SinceUTC { get; set; }
        public DateTime ToUTC { get; set; }
    }
}
