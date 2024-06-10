using OnlineBookStore_Ass.Data.Repository.Interface;
using OnlineBookStore_Ass.Domain.DTO;
using OnlineBookStore_Ass.Domain.Entity;
using OnlineBookStore_Ass.Domain.Enums;
using OnlineBookStore_Ass.Services.CacheServices.Interface;
using OnlineBookStore_Ass.Services.General;
using OnlineBookStore_Ass.Services.LoggingServices.Interface;
using OnlineBookStore_Ass.Services.PurchasesServices.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.PurchasesServices.Implementation
{
    public class PurchaseServices : IPurchaseServices
    {
        private readonly IRepository _repository;
        private readonly ICacheServices _cacheServices;
        private readonly ILoggingServices _logging;
        public PurchaseServices(IRepository repository, ILoggingServices logging, ICacheServices cacheServices)
        {
            _repository = repository;
            _logging = logging;
            _cacheServices = cacheServices;
        }
        public async Task<string> Add(PurchaseDTO entityDTO, string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "PurchaseServices.Add";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            Purchase entity = new Purchase()
            {
                id = entityDTO.userId,
                booksId = entityDTO.booksId,
                userId = entityDTO.userId,
                paymentOption = entityDTO.paymentOption,
            };
            int response = await _repository.Add<Purchase>(entity, "Purchases");
            if (response > 0)
            {
                logging.description = $"Purchase({JSONize.SerializeToString(entity)}) was Added Successfully";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return "Successful";

            }
            else
            {
                logging.description = $"Purchase({JSONize.SerializeToString(entity)}) Addition failed";
                logging.type = LogType.Error;
                _logging.SaveData(logging);
                return "not Successful";
            }
        }
        public async Task<string> Update(PurchaseDTO entityDTO, string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "PurchaseServices.Update";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            Purchase entity = new Purchase()
            {
                id = entityDTO.userId,
                booksId = entityDTO.booksId,
                userId = entityDTO.userId,
                paymentOption = entityDTO.paymentOption,
            };
            int response = await _repository.Update<Purchase>(entity, "Purchases");
            if (response > 0)
            {
                logging.description = $"Purchase({JSONize.SerializeToString(entity)}) Updated Succesfully";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return "cart Updated Succesfully";
            }
            logging.description = $"Purchgase({JSONize.SerializeToString(entity)}) Update Failed";
            logging.type = LogType.Error;
            _logging.SaveData(logging);
            return "Cart Update Failed";
        }
        public async Task<Purchase> GetAllMyPurchase(int id, string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "PurchaseServices.GetAll";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            Purchase purchase = await _cacheServices.GetAsync<Purchase>($"Purchase{id}");
            if (purchase != null)
            {
                logging.description = $"{purchase.books.Count()} is returned from cache";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return purchase;
            }
            purchase = await _repository.GetById<Purchase>(id, "Purchases");
            _ = _cacheServices.SetAsync<Purchase>($"Purchase{id}", purchase, TimeSpan.FromDays(1));
            if (purchase == null)
            {
                purchase = new Purchase();
                logging.description = "0 cart is added";
                logging.type = LogType.Error;
                _logging.SaveData(logging);
                return purchase;
            }
            logging.description = $"{purchase.books.Count()} is returned";
            logging.type = LogType.Succesfull;
            _logging.SaveData(logging);
            return purchase;
        }
        public async Task<IQueryable<Purchase>> GetAllOnWeb( string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "PurchaseServices.GetAll";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            IQueryable<Purchase> purchases = await _cacheServices.GetAsync<IQueryable<Purchase>>($"PurchaseAll");
            if (purchases != null)
            {
                logging.description = $"{purchases.Count()} is returned from cache";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return purchases;
            }
            purchases = await _repository.GetAll<Purchase>("Purchases");
            _ = _cacheServices.SetAsync<IQueryable<Purchase>>($"PurchaseAll", purchases, TimeSpan.FromDays(1));
            if (purchases == null)
            {
                logging.description = "0 cart is added";
                logging.type = LogType.Error;
                _logging.SaveData(logging);
                return purchases;
            }
            logging.description = $"{purchases.Count()} is returned";
            logging.type = LogType.Succesfull;
            _logging.SaveData(logging);
            return purchases;
        }
    }
}
