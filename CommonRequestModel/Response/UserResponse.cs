using CommonRequestModel.Request;
using System;

namespace CommonRequestModel.Response
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string EmailId { get; set; }
        public string Phone { get; set; }
        public string CompanyId { get; set; }
        public string ImgPath { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }
        public RoleRequest Role { get; set; }
        public StatusRequest Status { get; set; }
    }
}
