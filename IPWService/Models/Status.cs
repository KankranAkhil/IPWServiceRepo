using System;
using System.Collections.Generic;

namespace IPWService.Models
{
    public partial class Status
    {
        public Status()
        {
            Functionalities = new HashSet<Functionalities>();
            RoleFunctionMapping = new HashSet<RoleFunctionMapping>();
            Roles = new HashSet<Roles>();
            Users = new HashSet<Users>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; }

        public ICollection<Functionalities> Functionalities { get; set; }
        public ICollection<RoleFunctionMapping> RoleFunctionMapping { get; set; }
        public ICollection<Roles> Roles { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
