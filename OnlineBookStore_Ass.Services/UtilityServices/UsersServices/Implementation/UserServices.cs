using OnlineBookStore_Ass.Data.Repository.Interface;
using OnlineBookStore_Ass.Domain.DTO;
using OnlineBookStore_Ass.Domain.Entity;
using OnlineBookStore_Ass.Domain.Enums;
using OnlineBookStore_Ass.Services.BooksServices.Interface;
using OnlineBookStore_Ass.Services.CacheServices.Interface;
using OnlineBookStore_Ass.Services.General;
using OnlineBookStore_Ass.Services.LoggingServices.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.UsersServices.Implementation
{
    public class UserServices : IUserServices
    {
        private readonly IRepository _repository;
        private readonly ICacheServices _cacheServices;
        private readonly ILoggingServices _logging;
        public UserServices(IRepository repository, ILoggingServices logging, ICacheServices cacheServices)
        {
            _repository = repository;
            _logging = logging;
            _cacheServices = cacheServices;
        }
        public async Task<string> Add(UserDTO entityDTO, string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "UserServices.Add";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            User entity = new User()
            {
                id = int.Parse(entityDTO.id),
                username = entityDTO.username,
                password = entityDTO.password,
                email = entityDTO.email,
                fullName = entityDTO.fullName,
                userType = entityDTO.userType,
            };
            int response = await _repository.Add<User>(entity, "Users");
            if (response > 0)
            {
                logging.description = $"User({JSONize.SerializeToString(entity)}) was Added Successfully";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return "Successful";

            }
            else
            {
                logging.description = $"User({JSONize.SerializeToString(entity)}) Addition failed";
                logging.type = LogType.Danger;
                _logging.SaveData(logging);
                return "not Successful";
            }
        }

        public async Task<User> GetById(int id, string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "UserServices.GetById";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            User user = await _repository.GetById<User>(id, "Users");
            if (user == null)
            {
                user = new User();
                logging.description = $"user with id{id} does not exist";
                logging.type = LogType.Error;
                _logging.SaveData(logging);
                return user;
            }
            logging.description = $"{JSONize.SerializeToString(user)} is returned";
            logging.type = LogType.Succesfull;
            _logging.SaveData(logging);
            return user;
        }
        public async Task<IQueryable<User>> GetAll( string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "UserServices.GetAll";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            IQueryable<User> users = await _cacheServices.GetAsync<IQueryable<User>>("Users");
            if (users != null)
            {
                logging.description = $"{users.Count()} is returned from cache";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return users;
            }
            users = await _repository.GetAll<User>("Users");
            if(users != null)
            {
                _cacheServices.SetAsync<IQueryable<User>>("Books", users, TimeSpan.FromDays(1));
            }
            if (users == null)
            {
                users = new List<User>().AsQueryable();
                logging.description = "0 book is added";
                logging.type = LogType.Error;
                _logging.SaveData(logging);
                return users;
            }
            logging.description = $"{users.Count()} is returned";
            logging.type = LogType.Succesfull;
            _logging.SaveData(logging);
            return users;
        }
        public async Task<string> Update(UserDTO entityDTO, string oldPassword, string caller)
        {
            Logging logging = new Logging();
            logging.id = Guid.NewGuid();
            logging.source = "UserServices.Update";
            logging.controller = caller;
            logging.logTime = DateTime.Now;
            User entity = new User()
            {
                id = int.Parse(entityDTO.id),
                username = entityDTO.username,
                password = entityDTO.password,
                email = entityDTO.email,
                fullName = entityDTO.fullName,
                userType = entityDTO.userType,
            };
            int response = await _repository.Update<User>(entity, "Users");
            if (response > 0)
            {
                logging.description = $"User({JSONize.SerializeToString(entity)}) Updated Succesfully";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return "User Updated Succesfully";
            }
            logging.description = $"User({JSONize.SerializeToString(entity)}) Update Failed";
            logging.type = LogType.Error;
            _logging.SaveData(logging);
            return "User Update Failed";
        }
    }
}
