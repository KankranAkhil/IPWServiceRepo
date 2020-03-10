using System;
using System.Collections.Generic;
using System.Text;

namespace CommonRequestModel.Request
{
    public class RoleFunctionMappingRequest
    {
        public int FunctionId { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }
        public int Id { get; set; }
    }
}
