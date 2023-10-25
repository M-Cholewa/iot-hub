namespace iot_hub_backend
{
    public class TokenGenerationRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = "?";
        public List<string> Roles { get; set; } = new List<string>();
        public Dictionary<string, bool> CustomClaims { get; set; } = new();
    }
}
