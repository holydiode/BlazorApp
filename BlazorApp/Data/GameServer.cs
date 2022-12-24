using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace BlazorApp.Data
{
    public partial class GameServer
    {
        public GameServer()
        {
            Permissions = new HashSet<Permission>();
        }

        public int ServerId { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public int? Port { get; set; }
        public string Discrib { get; set; }
        public int CoreId { get; set; }

        [JsonIgnore]
        public virtual Core Core { get; set; }
        [JsonIgnore]
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
