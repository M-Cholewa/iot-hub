using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Business.Core.Device.Commands;
using Communication.MQTT;
using Domain.MQTT;
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

            var rpcCall = await _rpcClient.CallMethodAsync(request.DeviceId.ToString(), request.MethodName, request.Payload, cancellationToken);
            Console.WriteLine(" [.] Got '{0}'", rpcCall);

            bool _success = (rpcCall.result == RpcResult.SUCCESS);

            return new ExecuteDirectMethodCommandResult { IsSuccess = _success, ResultMsg = rpcCall.result.ToString(), RpcResponse = rpcCall.response };
        }
    }

}
