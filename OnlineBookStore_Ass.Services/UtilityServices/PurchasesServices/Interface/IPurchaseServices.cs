using OnlineBookStore_Ass.Domain.DTO;
using OnlineBookStore_Ass.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.PurchasesServices.Interface
{
    public interface IPurchaseServices
    {

        Task<string> Add(PurchaseDTO entity, string caller);
        Task<string> Update(PurchaseDTO entityDTO, string caller);
        Task<Purchase> GetAllMyPurchase(int id, string caller);
        Task<IQueryable<Purchase>> GetAllOnWeb(string caller);
    }
}
