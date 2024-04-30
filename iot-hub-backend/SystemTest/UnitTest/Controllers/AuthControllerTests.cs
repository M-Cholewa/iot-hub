using iot_hub_backend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Business.Core.Auth.Commands;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using iot_hub_backend.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;

namespace SystemTest.UnitTest.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task LoginTest()
        {
            // Utworzenie obiektów potrzebnych do testu
            var loginCommand = new LoginCommand { Email = "test", Password = "test" };
            var jwtSettings = new JwtSettings { Audience = "strona", Issuer = "serwer", Key = "KluczJsonWebToken" };

            // Utworzenie mocka mediatora
            var mockMediatr = new Mock<IMediator>();
            mockMediatr
                .Setup(m => m.Send(loginCommand, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new LoginCommandResult { IsSuccess = true, User = new Domain.Core.User { Email = "test" } });

            // Utworzenie kontrolera
            var controller = new AuthController(mockMediatr.Object, jwtSettings);
            var res = await controller.Login(loginCommand);

            // Sprawdzenie czy odpowiedź jest poprawna
            Assert.NotNull(res);
            Assert.NotNull(res.Value);
            Assert.NotNull(res.Value.Token);
            Assert.NotNull(res.Value.User);
            Assert.Equal("test", res.Value.User.Email);

        }
    }
}