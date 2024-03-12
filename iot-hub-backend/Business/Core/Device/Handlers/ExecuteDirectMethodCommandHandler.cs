using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Business.Core.Device.Commands;
using Business.InfluxRepository;
using Business.Repository;
using Communication.MQTT;
using Domain.InfluxDB;
using Domain.MQTT;
using MediatR;

namespace Business.Core.Device.Handlers
{
    public class ExecuteDirectMethodCommandHandler : IRequestHandler<ExecuteDirectMethodCommand, ExecuteDirectMethodCommandResult>
    {
        private readonly IRpcClient _rpcClient;
        private readonly DeviceRepository _deviceRepository;
        private readonly ConsoleRecordRepository _consoleRecordRepository;
        private readonly GeneralLogRepository _generalLogRepository;

        public ExecuteDirectMethodCommandHandler(IRpcClient rpcClient, DeviceRepository deviceRepository, ConsoleRecordRepository consoleRecordRepository, GeneralLogRepository generalLogRepository)
        {
            _rpcClient = rpcClient;
            _deviceRepository = deviceRepository;
            _consoleRecordRepository = consoleRecordRepository;
            _generalLogRepository = generalLogRepository;
        }

        public async Task<ExecuteDirectMethodCommandResult> Handle(ExecuteDirectMethodCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.DeviceId);

            if (device == null)
            {
                return new ExecuteDirectMethodCommandResult { IsSuccess = false, ResultMsg = "Device does not exist" };
            }

            var rpcCall = await _rpcClient.CallMethodAsync(device.MQTTUser!.ClientID.ToString(), request.MethodName, request.Payload, cancellationToken);
            Console.WriteLine(" [.] Got '{0}'", rpcCall);

            // save record to influx

            var consoleRecord = new Domain.InfluxDB.ConsoleRecord
            {
                DeviceId = request.DeviceId,
                RpcResult = rpcCall.result.ToString(),
                ResponseDataJson = rpcCall.response?.ResponseDataJson ?? "",
                DateUTC = DateTime.UtcNow
            };

            _consoleRecordRepository.Add(consoleRecord);

            bool _success = (rpcCall.result == RpcResult.SUCCESS);

            if (_success)
            {
                var generalLog = new GeneralLog
                {
                    DeviceId = device.Id,
                    DateUTC = DateTime.UtcNow,
                    Message = "Received RPI response"
                };

                _generalLogRepository.Add(generalLog);
            }


            return new ExecuteDirectMethodCommandResult { IsSuccess = _success, ResultMsg = rpcCall.result.ToString(), RpcResponse = rpcCall.response };
        }
    }

}
