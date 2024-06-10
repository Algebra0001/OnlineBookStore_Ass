using OnlineBookStore_Ass.Domain.Entity;
using OnlineBookStore_Ass.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Domain.DTO;
public class PurchaseDTO
{
    public List<string> booksId { get; set; }
    public int userId { get; set; }
    public PaymentOptions paymentOption { get; set; }
}