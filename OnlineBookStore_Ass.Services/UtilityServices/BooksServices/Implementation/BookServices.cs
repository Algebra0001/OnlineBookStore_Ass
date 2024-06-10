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
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static System.Reflection.Metadata.BlobBuilder;

namespace OnlineBookStore_Ass.Services.BooksServices.Implementation;
public class BookServices : IBookServices
{
    private readonly IRepository _repository;
    private readonly ICacheServices _cacheServices;
    private readonly ILoggingServices _logging;
    public BookServices(IRepository repository, ILoggingServices logging, ICacheServices cacheServices)
    {
        _repository = repository;
        _logging = logging;
        _cacheServices = cacheServices;
    }

    public async Task<string> Add(BookDTO entityDTO, string caller)
    {
        Logging logging = new Logging();
        logging.id = Guid.NewGuid();
        logging.source = "BookServices.Add";
        logging.controller = caller;
        logging.logTime = DateTime.Now;
        //IQueryable<Book> books = await _repository.GetAll();
        //Book bookCheck = books.FirstOrDefault(u => u.iSBN == entity.iSBN);
        //if (bookCheck == null)
        //{
        Book entity = new Book()
        {
            title = entityDTO.title,
            description = entityDTO.description,
            author = entityDTO.author,
            iSBN = entityDTO.iSBN,
            publicationYear = entityDTO.publicationYear,
            price = entityDTO.price,
        };
            int response = await _repository.Add<Book>(entity, "Books");
            if (response > 0)
            {
                logging.description = $"Book({JSONize.SerializeToString(entity)}) was Added Successfully";
                logging.type = LogType.Succesfull;
                _logging.SaveData(logging);
                return "Successful";

            }
            else
            {
                logging.description = $"book({JSONize.SerializeToString(entity)}) Addition failed";
                logging.type = LogType.Danger;
                _logging.SaveData(logging);
                return "not Successful";
            }

        //}
        //logging.description = "book exist and failed";
        //logging.type = LogType.Danger;
        //_logging.SaveData(logging);
        //return "book exist and failed";

    }

    public async Task<string> Delete(int id, string caller)
    {
        Logging logging = new Logging();
        logging.id = Guid.NewGuid();
        logging.source = "BookServices.Delete";
        logging.controller = caller;
        logging.logTime = DateTime.Now;
        int response = await _repository.Delete(id, "Books");
        if (response > 0)
        {
            logging.description = $"Book with id{id} Deleted Succesfully";
            logging.type = LogType.Succesfull;
            _logging.SaveData(logging);
            return "Successful";

        }
        else
        {
            logging.description = $"Book with id{id} Delete Failed";
            logging.type = LogType.Danger;
            _logging.SaveData(logging);
            return "Book Delete failed";
        }

    }

    public async Task<IQueryable<Book>> GetAll(string caller)
    {
        Logging logging = new Logging();
        logging.id = Guid.NewGuid();
        logging.source = "BookServices.GetAll";
        logging.controller = caller;
        logging.logTime = DateTime.Now;
        IQueryable<Book> books = await _cacheServices.GetAsync<IQueryable<Book>>("Books");
        if(books!=null)
        {
            logging.description = $"{books.Count()} is returned from cache";
            logging.type = LogType.Succesfull;
            _logging.SaveData(logging);
            return books;
        }
        books = await _repository.GetAll<Book>("Books");
        _ = _cacheServices.SetAsync<IQueryable<Book>>("Books", books, TimeSpan.FromDays(1));
        if (books == null)
        {
            books= new List<Book>().AsQueryable();
            logging.description = "0 book is added";
            logging.type = LogType.Error;
            _logging.SaveData(logging);
            return books;
        }
        logging.description = $"{books.Count()} is returned";
        logging.type = LogType.Succesfull;
        _logging.SaveData(logging);
        return books;
    }

    public async Task<Book> GetById(int id, string caller)
    {
        Logging logging = new Logging();
        logging.id = Guid.NewGuid();
        logging.source = "BookServices.GetById";
        logging.controller = caller;
        logging.logTime = DateTime.Now;
        Book book = await _repository.GetById<Book>(id,"Books");
        if (book == null)
        {
            book = new Book();
            logging.description = $"book with id{id} does not exist";
            logging.type = LogType.Error;
            _logging.SaveData(logging);
            return book;
        }
        logging.description = $"{JSONize.SerializeToString(book)} is returned";
        logging.type = LogType.Succesfull;
        _logging.SaveData(logging);
        return book;

    }

    public async Task<string> Update(BookDTO entityDTO, string caller)
    {
        Logging logging = new Logging();
        logging.id = Guid.NewGuid();
        logging.source = "BookServices.Update";
        logging.controller = caller;
        logging.logTime = DateTime.Now;
        Book entity = new Book()
        {
            id=int.Parse(entityDTO.id),
            title = entityDTO.title,
            description = entityDTO.description,
            author = entityDTO.author,
            iSBN = entityDTO.iSBN,
            publicationYear = entityDTO.publicationYear,
            price = entityDTO.price,
        };
        int response = await _repository.Update<Book>(entity, "Books");
        if (response>0)
        {
            logging.description = $"book({JSONize.SerializeToString(entity)}) Updated Succesfully";
            logging.type = LogType.Succesfull;
            _logging.SaveData(logging);
            return "book Updated Succesfully";
        }
        logging.description = $"Book({JSONize.SerializeToString(entity)}) Update Failed";
        logging.type = LogType.Error;
        _logging.SaveData(logging);
        return "Book Update Failed";
    }
}

