using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace BlazorApp.Data
{
    public partial class Player
    {
        public Player()
        {
            Permissions = new HashSet<Permission>();
            ValidRoles = new HashSet<ValidRole>();
        }

        public int PlayerId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Nikname { get; set; }
        public int? Balance { get; set; }

        [JsonIgnore]
        public virtual ICollection<Permission> Permissions { get; set; }
        [JsonIgnore]
        public virtual ICollection<ValidRole> ValidRoles { get; set; }
    }
}
