using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot_hub_backend.Infrastructure.Security
{
    public static class Policies
    {
        public const string UserPolicyName = "IsUser";
    }

    public static class ClaimNames
    {
        public const string UserId = "userid";

    }
    public static class Roles
    {
        public const string Admin = "ADMIN";
        public const string User = "USER";
    }
}
