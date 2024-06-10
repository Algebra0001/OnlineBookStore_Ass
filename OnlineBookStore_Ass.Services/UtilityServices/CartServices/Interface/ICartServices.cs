using OnlineBookStore_Ass.Domain.DTO;
using OnlineBookStore_Ass.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.CacheServices.Interface
{
    public interface ICartServices
    {

        Task<Cart> Get(int id, string caller);
        Task<string> Add(CartDTO entityDTO, string caller);
        Task<string> Update(CartDTO entityDTO, string caller);
        Task<string> Delete(int id, string caller);

    }
}
