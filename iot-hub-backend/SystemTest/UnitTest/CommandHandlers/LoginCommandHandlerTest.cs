using Business.Core.Auth.Commands;
using Business.Core.Auth.Handlers;
using Business.Infrastructure.Security;
using Business.Interface;
using Domain.Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTest.UnitTest.CommandHandlers
{
    public class LoginCommandHandlerTest
    {
        [Fact]
        public async Task Handle()
        {
            // hasher hasła
            var passwordHasher = new PasswordHasher();

            // stworzenie użytkownika
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test",
                PasswordHash = passwordHasher.HashPassword("test")
            };

            // udawane repozytorium
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.GetByEmailAsync("test")).ReturnsAsync(user);

            // poprawne logowanie
            var handler = new LoginCommandHandler(passwordHasher, userRepo.Object);
            var res = await handler.Handle(new LoginCommand { Email = "test", Password = "test" }, CancellationToken.None);
            Assert.True(res.IsSuccess);

            // niepoprawne logowanie
            res = await handler.Handle(new LoginCommand { Email = "test", Password = "wrong" }, CancellationToken.None);
            Assert.False(res.IsSuccess);
        }
    }
}
