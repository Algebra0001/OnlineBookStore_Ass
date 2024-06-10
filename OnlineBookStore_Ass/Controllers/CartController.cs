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
    public class CartController : ControllerBase
    {
        private readonly IOnlineBookStoreMainServices _onlineBookStoreMainServices;
        private readonly string _controller;
        public CartController(IOnlineBookStoreMainServices onlineBookStoreMainServices) 
        {
            _onlineBookStoreMainServices = onlineBookStoreMainServices;
            _controller = "CartController";
        }

        [HttpPost("AddBooksCart")]
        public async Task<IActionResult> AddBookCart(List<int> booksId)
        {
            string response = await _onlineBookStoreMainServices.AddCarts(booksId, _controller);
            return Ok(Response);
        }
        [HttpPost("AddBookCart")]
        public async Task<IActionResult> AddBookCart(int bookId)
        {
            string response = await _onlineBookStoreMainServices.AddCart(bookId, _controller);
            return Ok(Response);
        }
        [HttpGet("GetBooksInCart")]
        public async Task<IActionResult> GetCart()
        {
            Cart response = await _onlineBookStoreMainServices.GetBooksInCart(_controller);
            return Ok(Response);
        }
        [HttpPut("DeleteBookInCart")]
        public async Task<IActionResult> DeleteFromCart(int bookId)
        {
            string response = await _onlineBookStoreMainServices.RemoveFromCart(bookId,_controller);
            return Ok(Response);
        }

    }
}
