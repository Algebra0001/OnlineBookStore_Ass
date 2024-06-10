using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Domain.Entity
{
    public class Cart
    {
        public int userId { get; set; }
        public List<string> bookId { get; set; }
        //Navigation Properties
        public ICollection<Book> books { get; set; } = new List<Book>();
        public User user { get; set; } = new User();

    }
}
