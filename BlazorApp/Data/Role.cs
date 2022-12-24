using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace BlazorApp.Data
{
    public partial class Role
    {
        public Role()
        {
            InverseInherited = new HashSet<Role>();
            Permissions = new HashSet<Permission>();
            ValidRoles = new HashSet<ValidRole>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public int? InheritedId { get; set; }

        [JsonIgnore]
        public virtual Role Inherited { get; set; }
        [JsonIgnore]
        public virtual ICollection<Role> InverseInherited { get; set; }
        [JsonIgnore]
        public virtual ICollection<Permission> Permissions { get; set; }
        [JsonIgnore]
        public virtual ICollection<ValidRole> ValidRoles { get; set; }
    }
}
