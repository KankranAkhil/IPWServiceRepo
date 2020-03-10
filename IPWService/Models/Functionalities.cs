using System;
using System.Collections.Generic;

namespace IPWService.Models
{
    public partial class Functionalities
    {
        public Functionalities()
        {
            RoleFunctionMapping = new HashSet<RoleFunctionMapping>();
        }

        public int FunctionalityId { get; set; }
        public string Name { get; set; }
        public int? StatusId { get; set; }

        public Status Status { get; set; }
        public ICollection<RoleFunctionMapping> RoleFunctionMapping { get; set; }
    }
}
