using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.Data.SqlClient;

namespace OnlineBookStore_Ass.Data
{
    public class DbContext : IDbContext
    {
        private readonly string connectionString;

        public DbContext(string connectionString)
        {

            this.connectionString = connectionString;
        }
        public IDbConnection Connection => new NpgsqlConnection(connectionString);
    }
}
