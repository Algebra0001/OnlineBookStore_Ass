using OnlineBookStore_Ass.Domain.DTO;
using OnlineBookStore_Ass.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.OnlineBookStore_AssServices.Interface
{
    public interface IOnlineBookStoreMainServices
    {

        Task<string> AddCart( int bookId, string Controller);
        Task<string> AddnewBook(BookDTO bookDTO, string Controller);
        Task<string> AddUser(UserDTO userDTO, string Controller);
        Task<string> DeleteBook(int bookId, string Controller);
        Task<Book> GetBook(int bookId, string Controller);
        Task<IEnumerable<Book>> GetBooks(string Controller);
        Task<Cart> GetBooksInCart( string Controller);
        Task<Purchase> GetPurchase( string Controller);
        Task<IEnumerable<Purchase>> GetPurchases(string Controller);
        Task<User> GetUser(int id, string Controller);
        Task<IEnumerable<User>> GetUsers(string Controller);
        Task<string> Login(string userDetails,string Password, string Controller);
        Task<string> Logout();
        Task<string> AddCarts(List<int> booksId, string Controller);
        Task<string> Purchase( string paymentOption, string bookId, string Controller);
        Task<string> Purchase(CartDTO cartDTOs, string Controller);
        Task<string> RemoveFromCart(int id, string Controller);
        Task<IEnumerable<Book>> SearchBook(string parameter, string Controller);
        Task<string> UpdateBook(BookDTO bookDTO, string Controller);
        Task<string> UpdateCart(CartDTO cartDTO, string Controller);
        Task<string> UpdateUser(UserDTO userDTO, string oldPasword, string Controller);
        Task<string> ConfrimLogin(string Controller);
    } 
}
