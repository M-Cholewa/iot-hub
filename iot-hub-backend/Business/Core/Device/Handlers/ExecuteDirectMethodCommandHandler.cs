using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Business.Core.Device.Commands;
using Communication.MQTT;
using MediatR;

namespace Business.Core.Device.Handlers
{
    public class ExecuteDirectMethodCommandHandler : IRequestHandler<ExecuteDirectMethodCommand, ExecuteDirectMethodCommandResult>
    {
        private readonly IRpcClient _rpcClient;

        public ExecuteDirectMethodCommandHandler(IRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        public async Task<ExecuteDirectMethodCommandResult> Handle(ExecuteDirectMethodCommand request, CancellationToken cancellationToken)
        {

            //Console.WriteLine(" [x] Requesting fib({0})", n);
            var response = await _rpcClient.CallMethodAsync("esp-32-device", request.MethodName, request.Payload);
            Console.WriteLine(" [.] Got '{0}'", response);

            return new ExecuteDirectMethodCommandResult { IsSuccess = false };
        }
    }

}
