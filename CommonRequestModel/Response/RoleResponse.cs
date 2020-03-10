using CommonRequestModel.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonRequestModel.Response
{
    public class RoleResponse
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public StatusRequest Status { get; set; }
    }
}
