using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorApp.Data
{
    public partial class Core
    {
        public Core()
        {
            GameServers = new HashSet<GameServer>();
        }

        public int CoreId { get; set; }
        public string Name { get; set; }
        public string GameVersion { get; set; }
        public string Version { get; set; }

        public virtual ICollection<GameServer> GameServers { get; set; }
    }
}
