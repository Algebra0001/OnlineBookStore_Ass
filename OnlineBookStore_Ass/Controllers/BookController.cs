using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore_Ass.Domain.DTO;
using OnlineBookStore_Ass.Domain.Entity;
using OnlineBookStore_Ass.Domain.Enums;
using OnlineBookStore_Ass.Services.OnlineBookStore_AssServices.Implementation;
using OnlineBookStore_Ass.Services.OnlineBookStore_AssServices.Interface;
using OnlineBookStore_Ass.Services.UtilityServices.General;

namespace OnlineBookStore_Ass.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IOnlineBookStoreMainServices _onlineBookStoreMainServices;
        private readonly string _controller;
        public BookController(IOnlineBookStoreMainServices onlineBookStoreMainServices) 
        {
            _onlineBookStoreMainServices = onlineBookStoreMainServices;
            _controller = "BookController";
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            IEnumerable<Book> response = await _onlineBookStoreMainServices.GetBooks(_controller);
            return Ok(Response);
        }
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(string title,string description,string author,string iSBN,string publicationYear,double price)
        {
            BookDTO bookDto=Mapper.InputMapperParameterToBookDTO(null,title,description,author,iSBN,publicationYear,price);
           string response = await _onlineBookStoreMainServices.AddnewBook(bookDto,_controller);
            return Ok(Response);
        }
        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook(string id, string title, string description, string author, string iSBN, string publicationYear, double price)
        {
            BookDTO bookDTO = Mapper.InputMapperParameterToBookDTO(id, title, description, author, iSBN, publicationYear, price);
            string response = await _onlineBookStoreMainServices.UpdateBook(bookDTO, _controller);
            return Ok(Response);
        }
        [HttpGet("GetBook")]
        public async Task<IActionResult> GetBook(int bookId)
        {
            Book response = await _onlineBookStoreMainServices.GetBook(bookId, _controller);
            return Ok(response);
        }
        [HttpGet("SearchBook")]
        public async Task<IActionResult> SearchBook(string SearchKeyword)
        {
            IEnumerable<Book> response = await _onlineBookStoreMainServices.SearchBook(SearchKeyword, _controller);
            return Ok(response);
        }
        [HttpGet("DeleteBooks")]
        public async Task<IActionResult> DeleteBooks(int bookId)
        {
            string response = await _onlineBookStoreMainServices.DeleteBook(bookId,_controller);
            return Ok(Response);
        }

    }
}
