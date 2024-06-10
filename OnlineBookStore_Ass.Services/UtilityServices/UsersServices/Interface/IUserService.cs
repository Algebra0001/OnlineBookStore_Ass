using OnlineBookStore_Ass.Domain.DTO;
using OnlineBookStore_Ass.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.BooksServices.Interface
{
    public interface IUserServices
    {
        //Task<IQueryable<User>> GetAll();
        Task<User> GetById(int id, string caller);
        Task<string> Add(UserDTO entityDTO, string caller);
        Task<string> Update(UserDTO entityDTO, string oldPassword, string caller);
        Task<IQueryable<User>> GetAll(string caller);
    }
}
