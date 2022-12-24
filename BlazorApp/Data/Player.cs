using System;
using System.Collections.Generic;

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

        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<ValidRole> ValidRoles { get; set; }
    }
}
