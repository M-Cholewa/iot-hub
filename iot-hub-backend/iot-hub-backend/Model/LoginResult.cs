using Domain.Core;

namespace iot_hub_backend.Model
{
    public class LoginResult
    {
        public string? Token { get; set; }
        public User? User { get; set; }
    }
}
