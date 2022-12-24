using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace BlazorApp.Data
{
    public partial class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Value { get; set; }
        public int? ServerId { get; set; }
        public int? RoleId { get; set; }
        public int? PlayerId { get; set; }

        [JsonIgnore]
        public virtual Player Player { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }
        [JsonIgnore]
        public virtual GameServer Server { get; set; }
    }
}
