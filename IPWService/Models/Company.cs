using System;
using System.Collections.Generic;

namespace IPWService.Models
{
    public partial class Company
    {
        public Company()
        {
            Users = new HashSet<Users>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
