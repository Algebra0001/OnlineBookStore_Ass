using OnlineBookStore_Ass.Domain.DTO;
using OnlineBookStore_Ass.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.BooksServices.Interface
{
    public interface IBookServices
    {
        Task<IQueryable<Book>> GetAll(string controller);
        Task<Book> GetById(int id, string controller);
        Task<string> Add(BookDTO entity, string controller);
        Task<string> Update(BookDTO entity, string controller);
        Task<string> Delete(int id, string controller);
    }
}
