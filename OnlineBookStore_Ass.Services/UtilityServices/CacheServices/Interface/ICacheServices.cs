using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.CacheServices.Interface
{
    public interface ICacheServices 
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        Task<bool> AddAsync<T>(string key, T value, TimeSpan expiration);
        Task<bool> UpdateAsync<T>(string key, T value);
        Task<bool> DeleteAsync(string key);
    }
}
