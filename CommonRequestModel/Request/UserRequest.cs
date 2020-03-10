using System;

namespace CommonRequestModel.Request
{
    public class UserRequest
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
    }
}
