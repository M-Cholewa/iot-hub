using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Core
{
    public class MQTTUser
    {
        public Guid ClientID { get; set; } // ClientID
        public string? Username { get; set; }
        [JsonIgnore]
        public string? PasswordHash { get; set; }
    }
}
