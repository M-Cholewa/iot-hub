using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{

    public class Role
    {
        public const string Admin = "ADMIN";
        public const string User = "USER";

        public Guid Id { get; set; }
        public string? Key { get; set; }

    }
}
