using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Data.Repository.Interface;
public interface IRepository
{
    Task<IQueryable<T>> GetAll<T>(string tableName);
    Task<T> GetById<T>(int id, string tableName);
    Task<int> Add<T>(T entity, string tableName);
    Task<int> Update<T>(T entity, string tableName);
    Task<int> Delete(int id, string tableName);
}


