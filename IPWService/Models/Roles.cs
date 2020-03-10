using System;
using System.Collections.Generic;

namespace IPWService.Models
{
    public partial class Roles
    {
        public Roles()
        {
            RoleFunctionMapping = new HashSet<RoleFunctionMapping>();
            Users = new HashSet<Users>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public int? StatusId { get; set; }

        public Status Status { get; set; }
        public ICollection<RoleFunctionMapping> RoleFunctionMapping { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
