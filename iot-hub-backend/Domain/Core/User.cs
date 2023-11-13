using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public class User
    {
        public Guid Id { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public virtual ICollection<Role>? Roles { get; set; }

        public virtual ICollection<Device>? Devices { get; set; }
    }
}
