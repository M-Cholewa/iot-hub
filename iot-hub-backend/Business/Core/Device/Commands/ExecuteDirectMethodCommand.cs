using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Device.Commands
{
    public class ExecuteDirectMethodCommandResult
    {
        public bool IsSuccess { get; set; } = false;
        public string ResultMsg { get; set; } = "?";
    }

    public class ExecuteDirectMethodCommand : IRequest<ExecuteDirectMethodCommandResult>
    {
        public Guid DeviceId { get; set; }
        public string MethodName { get; set; } = "?";
        public string Payload { get; set; } = "?";

        public ExecuteDirectMethodCommand()
        {
        }
    }

}
