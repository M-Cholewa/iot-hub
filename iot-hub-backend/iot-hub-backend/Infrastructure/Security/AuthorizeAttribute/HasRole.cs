using Microsoft.AspNetCore.Mvc.Filters;

namespace iot_hub_backend.Infrastructure.Security.AuthorizeAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HasRole : RequiresClaim
    {
        public HasRole(string roleName) : base(roleName, "true")
        {
        }
    }
}
