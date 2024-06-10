using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Data
{
    public interface IDbContext
    {
        IDbConnection Connection { get; }
    }
}
