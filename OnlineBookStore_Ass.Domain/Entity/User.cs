using OnlineBookStore_Ass.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Domain.Entity;

public class User
{
    [Required]
    public int id { get; set; }
    [Required]
    public string username { get; set; }
    public Guid? LoginStatus { get; set; }
    [Required]
    public string password { get; set; }
    [Required]
    public string? email { get; set; }
    [Required]
    public string? fullName { get; set; }
    [Required]
    public Usertype userType { get; set; } = Usertype.User;
    public  Cart Cart { get; set; }= new Cart();
    public Purchase purchase { get; set; } = new Purchase();



}

