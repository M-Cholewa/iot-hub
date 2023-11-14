using Business.Core.Auth.Commands;
using Business.Infrastructure.Security;
using Business.Repository;
using MediatR;
using MQTTnet.Protocol;
using Npgsql.Replication.PgOutput.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Auth.Handlers
{
    public class MQTTLoginCommandHandler : IRequestHandler<MQTTLoginCommand, MQTTLoginCommandResult>
    {
        private readonly Business.Infrastructure.Security.IPasswordHasher _passHasher;
        private readonly MQTTUserRepository _mqttUserRepository;

        public MQTTLoginCommandHandler(IPasswordHasher passHasher, MQTTUserRepository mqttUserRepository)
        {
            _passHasher = passHasher;
            _mqttUserRepository = mqttUserRepository;
        }

        public async Task<MQTTLoginCommandResult> Handle(MQTTLoginCommand request, CancellationToken cancellationToken)
        {
            if (request.Username == null || request.Password == null)
            {
                return new MQTTLoginCommandResult() { MqttConnectReasonCode = MqttConnectReasonCode.BadUserNameOrPassword };
            }

            var usr = await _mqttUserRepository.GetByIdAsync(request.ClientId);

            if (usr == null)
            {
                // no user with this ID

                return new MQTTLoginCommandResult() { MqttConnectReasonCode = MqttConnectReasonCode.ClientIdentifierNotValid };
            }

            // check if username equals request.Username

            if (usr.Username?.Equals(request.Username) == false)
            {
                // wrong username

                return new MQTTLoginCommandResult() { MqttConnectReasonCode = MqttConnectReasonCode.BadUserNameOrPassword };
            }

            var isPwdCorrect = _passHasher.VerifyHashedPassword(usr.PasswordHash!, request.Password);

            if (!isPwdCorrect)
            {
                // wrong password

                return new MQTTLoginCommandResult() { MqttConnectReasonCode = MqttConnectReasonCode.BadUserNameOrPassword };
            }

            return new MQTTLoginCommandResult { MqttConnectReasonCode = MqttConnectReasonCode.Success };
        }
    }
}
