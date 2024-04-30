using Business.Core.Auth.Commands;
using FluentAssertions;
using iot_hub_backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SystemTest.Base;

namespace SystemTest.IntegrationTest.Controllers
{
    public class AuthController
    {
        [Fact]
        public async Task Login()
        {
            // Utwórz klienta HTTP
            var application = new WebAppFactory().CreateClient();

            // Utwórz obiekt LoginCommand
            var loginCommand = new LoginCommand
            {
                Email = "user",
                Password = "user"
            };

            // Wyślij żądanie POST do /Auth/Login
            var res = await application.PostAsJsonAsync("/Auth/Login", loginCommand);

            // Sprawdź, czy odpowiedź jest pomyślna
            res.EnsureSuccessStatusCode();

            // Odczytaj odpowiedź jako LoginResult
            var loginResponse = await res.Content.ReadFromJsonAsync<LoginResult>();

            // Sprawdź, czy odpowiedź zawiera poprawny adres email
            loginResponse?.User?.Email?.Should().Be("user");

            // Zwolnij zasoby
            application.Dispose();
        }
    }
}
