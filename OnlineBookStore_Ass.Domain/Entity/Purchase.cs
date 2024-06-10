using OnlineBookStore_Ass.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Domain.Entity;
public class Purchase
{
    [Required]
    public int id { get; set; }
    [Required]
    public List<string> booksId { get; set; }
    [Required]
    public int userId { get; set; }
    [Required]
    public PaymentOptions paymentOption { get; set; }
    //Navigation Properties
    public ICollection<Book> books { get; set; } = new List<Book>();
    public User user { get; set; } = new User();
}

