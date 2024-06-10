using OnlineBookStore_Ass.Data.Repository.Interface;
using OnlineBookStore_Ass.Domain.DTO;
using OnlineBookStore_Ass.Domain.Entity;
using OnlineBookStore_Ass.Domain.Enums;
using OnlineBookStore_Ass.Services.CacheServices.Interface;
using OnlineBookStore_Ass.Services.CacheServices.Interface;
using OnlineBookStore_Ass.Services.General;
using OnlineBookStore_Ass.Services.LoggingServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace OnlineBookStore_Ass.Services.CartServices.Implementation
{
    public class CartServices : ICartServices
    {
        private readonly IRepository _repository;
        private readonly ICacheServices _cacheServices;
        private readonly ILoggingServices _logging;
        public CartServices(IRepository repository, ILoggingServices logging, ICacheServices cacheServices)
        {
            _repository = repository;
            _logging = logging;
            _cacheServices = cacheServices;
        }
        public async Task<string> Add(CartDTO entityDTO, string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "CartServices.Add";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            Cart entity = new Cart()
            {
                userId = entityDTO.userId,
                bookId = entityDTO.bookId,

            };
            int response = await _repository.Add<Cart>(entity, "Carts");
            if (response > 0)
            {
                logging.description = $"Cart({JSONize.SerializeToString(entity)}) was Added Successfully";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return "Successful";

            }
            else
            {
                logging.description = $"Cart({JSONize.SerializeToString(entity)}) Addition failed";
                logging.type = LogType.Danger;
                _logging.SaveData(logging);
                return "not Successful";
            }
        }

        public async Task<string> Delete(int id, string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "CartServices.Delete";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            int response = await _repository.Delete(id, "Carts");
            if (response > 0)
            {
                logging.description = $"Cart with id{id} Deleted Succesfully";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return "Successful";

            }
            else
            {
                logging.description = $"Cart with id{id} Delete Failed";
                logging.type = LogType.Danger;
                _logging.SaveData(logging);
                return "Cart Delete failed";
            }
        }

        public async Task<Cart> Get(int id, string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "CartServices.GetAll";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            Cart cart = await _cacheServices.GetAsync<Cart>("Cart");
            if (cart != null)
            {
                logging.description = $"{cart.books.Count()} is returned from cache";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return cart;
            }
            cart = await _repository.GetById<Cart>(id, "Carts");
            _cacheServices.SetAsync<Cart>("Carts", cart, TimeSpan.FromDays(1));
            if (cart == null)
            {
                cart = new Cart();
                logging.description = "0 cart is added";
                logging.type = LogType.Error;
                _logging.SaveData(logging);
                return cart;
            }
            logging.description = $"{cart.books.Count()} is returned";
            logging.type = LogType.Succesfull;
            _logging.SaveData(logging);
            return cart;
        }

        public async Task<string> Update(CartDTO entityDTO, string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "CartServices.Update";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            Cart entity = new Cart()
            {
                userId = entityDTO.userId,
                bookId = entityDTO.bookId,


            };
            int response = await _repository.Update<Cart>(entity, "Carts");
            if (response > 0)
            {
                logging.description = $"cart({JSONize.SerializeToString(entity)}) Updated Succesfully";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return "cart Updated Succesfully";
            }
            logging.description = $"Cart({JSONize.SerializeToString(entity)}) Update Failed";
            logging.type = LogType.Error;
            _logging.SaveData(logging);
            return "Cart Update Failed";
        }
    }
}
