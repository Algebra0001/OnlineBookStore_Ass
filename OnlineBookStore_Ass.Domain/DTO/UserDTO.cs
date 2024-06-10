using OnlineBookStore_Ass.Domain.Entity;
using OnlineBookStore_Ass.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Domain.DTO
{
    public class UserDTO
    {
        public string? id { get; set; }

        public string username { get; set; }   
        public Guid? LoginStatus { get; set; }
        public string? password  { get; set; }
        public string? email { get; set; }
        public string? fullName { get; set; }
        public Usertype userType { get; set; }
    }
}
