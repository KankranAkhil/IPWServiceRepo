using System;
using System.Collections.Generic;
using System.Text;

namespace CommonRequestModel.Request
{
    public class RoleRequest
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
    }
}
