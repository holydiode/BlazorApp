using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorApp.Data
{
    public partial class ValidRole
    {
        public int ValidRoleId { get; set; }
        public int PlayerId { get; set; }
        public int RoleId { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public virtual Player Player { get; set; }
        public virtual Role Role { get; set; }
    }
}
