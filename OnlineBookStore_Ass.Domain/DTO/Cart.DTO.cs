using OnlineBookStore_Ass.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Domain.DTO
{
    public class CartDTO
    {
        public int userId { get; set; }
        public List<string> bookId { get; set; }
    }
}
