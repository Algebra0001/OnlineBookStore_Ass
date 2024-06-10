using OnlineBookStore_Ass.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Domain.Entity
{
    public class Logging
    {

        public Guid id { get; set; }
        public string controller { get; set; }
        public string description { get; set; }
        public DateTime logTime { get; set; }       
        public string source { get; set; }
        public LogType type { get; set; }
    }
}

