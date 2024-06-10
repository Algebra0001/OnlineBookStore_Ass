using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Domain.Entity
{
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public string? description { get; set; }
        public string author { get; set; }
        public string iSBN { get; set; }
        public string publicationYear { get; set; }        
        public double price { get; set; }
        //Navigation Properties
        public ICollection<Cart> carts { get; set;} = new List<Cart>();
        public ICollection<Purchase> purchases { get; set; } = new List<Purchase>();
    }
}
