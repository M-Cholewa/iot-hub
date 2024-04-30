using Domain.Core;

namespace iot_hub_backend.Model
{
    public class LoginResult
    {
        public string? Token { get; set; }
        public User? User { get; set; }

        public LoginResult(string? token, User? user)
        {
            Token = token;
            User = user;
        }

        public LoginResult()
        {
        }
    }
}
