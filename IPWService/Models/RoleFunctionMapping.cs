using System;
using System.Collections.Generic;

namespace IPWService.Models
{
    public partial class RoleFunctionMapping
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? FunctionId { get; set; }
        public int? StatusId { get; set; }

        public Functionalities Function { get; set; }
        public Roles Role { get; set; }
        public Status Status { get; set; }
    }
}
