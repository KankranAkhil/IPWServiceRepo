using System;
using System.Collections.Generic;

namespace IPWService.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string EmailId { get; set; }
        public string Phone { get; set; }
        public int? CompanyId { get; set; }
        public string ImgPath { get; set; }
        public int? StatusId { get; set; }
        public int? RoleId { get; set; }

        public Company Company { get; set; }
        public Roles Role { get; set; }
        public Status Status { get; set; }
    }
}
