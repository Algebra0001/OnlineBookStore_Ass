using Microsoft.Extensions.Configuration;
using OnlineBookStore_Ass.Services.CacheServices.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.CacheServices.Implementation;
    public class CacheServices : ICacheServices
    {
        private readonly ConnectionMultiplexer _redisConnection;
    public CacheServices(IConfiguration config)
    {
        _redisConnection = ConnectionMultiplexer.Connect(config["RedisConnection"]);
    }

        public async Task<T> GetAsync<T>(string key)
        {
            var db = _redisConnection.GetDatabase();
            var value = await db.StringGetAsync(key);
            return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var db = _redisConnection.GetDatabase();
            await db.StringSetAsync(key, JsonSerializer.Serialize(value), expiration);
        }

        public async Task<bool> AddAsync<T>(string key, T value, TimeSpan expiration)
        {
            var db = _redisConnection.GetDatabase();
            return await db.StringSetAsync(key, JsonSerializer.Serialize(value), expiration, When.NotExists);
        }

        public async Task<bool> UpdateAsync<T>(string key, T value)
        {
            var db = _redisConnection.GetDatabase();
            return await db.StringSetAsync(key, JsonSerializer.Serialize(value));
        }

        public async Task<bool> DeleteAsync(string key)
        {
            var db = _redisConnection.GetDatabase();
            return await db.KeyDeleteAsync(key);
        }
    }


