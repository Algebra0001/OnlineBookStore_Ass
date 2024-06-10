using Dapper;
using OnlineBookStore_Ass.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Data.Repository.Implementation
{
    public class Repository : IRepository
    {
        private readonly IDbContext _context;

        public Repository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IQueryable<T>> GetAll<T>(string tableName)
        {
            using (var dbConnection = _context.Connection)
            {
                string query = $"SELECT * FROM {$"public.\"{tableName}\""}";
                return (await dbConnection.QueryAsync<T>(query)).AsQueryable();
            }
        }

        public async Task<T> GetById<T>(int id, string tableName)
        {
            using (var dbConnection = _context.Connection)
            {
                string query = $"SELECT * FROM {$"public.\"{tableName}\""} WHERE Id = @Id";
                return (await dbConnection.QueryAsync<T>(query, new { Id = id })).FirstOrDefault();
            }
        }

        public async Task<int> Add<T>(T entity, string tableName)
        {
            using (var dbConnection = _context.Connection)
            {
                string insertQuery = GenerateInsertQuery<T>($"public.\"{tableName}\"");
                return await dbConnection.ExecuteAsync(insertQuery, entity);
            }
        }

        public async Task<int> Update<T>(T entity, string tableName)
        {
            using (var dbConnection = _context.Connection)
            {
                string updateQuery = GenerateUpdateQuery<T>($"public.\"{tableName}\"");
                return await dbConnection.ExecuteAsync(updateQuery, entity);
            }
        }

        public async Task<int> Delete(int id, string tableName)
        {
            using (var dbConnection = _context.Connection)
            {
                string query = $"DELETE FROM {$"public.\"{tableName}\""} WHERE Id = @Id";
                return await dbConnection.ExecuteAsync(query, new { Id = id });
            }
        }

        private string GenerateInsertQuery<T>(string tableName)
        {
            var properties = typeof(T).GetProperties().Select(p => p.Name);
            var columns = string.Join(", ", properties);
            var values = string.Join(", ", properties.Select(p => "@" + p));
            return $"INSERT INTO {$"public.\"{tableName}\""} ({columns}) VALUES ({values})";
        }

        private string GenerateUpdateQuery<T>(string tableName)
        {
            var properties = typeof(T).GetProperties().Select(p => p.Name);
            var setClause = string.Join(", ", properties.Select(p => $"{p} = @{p}"));
            return $"UPDATE {$"public.\"{tableName}\""} SET {setClause} WHERE Id = @Id";
        }
    }
}
