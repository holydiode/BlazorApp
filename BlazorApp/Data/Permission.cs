using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorApp.Data
{
    public partial class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Value { get; set; }
        public int ServerId { get; set; }
        public int? RoleId { get; set; }
        public int? PlayerId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Role Role { get; set; }
        public virtual GameServer Server { get; set; }
    }
}
