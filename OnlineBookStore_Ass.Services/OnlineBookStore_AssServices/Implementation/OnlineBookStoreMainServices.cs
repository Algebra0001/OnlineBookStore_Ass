using OnlineBookStore_Ass.Domain.DTO;
using OnlineBookStore_Ass.Domain.Entity;
using OnlineBookStore_Ass.Domain.Enums;
using OnlineBookStore_Ass.Services.BooksServices.Implementation;
using OnlineBookStore_Ass.Services.BooksServices.Interface;
using OnlineBookStore_Ass.Services.CacheServices.Interface;
using OnlineBookStore_Ass.Services.LoggingServices.Interface;
using OnlineBookStore_Ass.Services.OnlineBookStore_AssServices.Interface;
using OnlineBookStore_Ass.Services.PurchasesServices.Implementation;
using OnlineBookStore_Ass.Services.PurchasesServices.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.OnlineBookStore_AssServices.Implementation
{
    public class OnlineBookStoreMainServices : IOnlineBookStoreMainServices
    {
        private readonly ICartServices cartServices;
        private readonly ICacheServices cacheServices;
        private readonly IBookServices bookServices;
        private readonly ILoggingServices loggingServices;
        private readonly IPurchaseServices purchaseServices;
        private readonly IUserServices userServices;
        public OnlineBookStoreMainServices(ICartServices cartServices, ICacheServices cacheServices, IBookServices bookServices, ILoggingServices loggingServices, IPurchaseServices purchaseServices, IUserServices userServices)
        {
            this.cartServices = cartServices;
            this.cacheServices = cacheServices;
            this.bookServices = bookServices;
            this.purchaseServices = purchaseServices;
            this.userServices = userServices;
            this.loggingServices = loggingServices;
        }
        public async Task<string> AddCarts(List<int> booksId, string Controller)
        {

            string userDetails = await ConfrimLogin(Controller);
            string response = "";
            CartDTO cartDTO = new CartDTO();
            User user = new User();
            IQueryable<User> usersGet = await userServices.GetAll($"{Controller}.GetBooksInCart");
            if (string.IsNullOrEmpty(Controller) || string.IsNullOrEmpty(userDetails) || booksId.Count < 0)
            {
                response = "Please supply the correct value and user or bookId is not correct";
            }
            else
            {
                user = usersGet.FirstOrDefault(u => (u.email == userDetails) || (u.username == userDetails));

                if (user == null)
                {
                    response = "Please supply the correct value and user does not exist";
                }
                else
                {
                    Cart getCart = await cartServices.Get(user.id, Controller + ".AddCart");
                    foreach (int bookId in booksId)
                    {
                        if (getCart == null)
                        {
                            getCart.bookId.Add(bookId.ToString());
                            cartDTO = new CartDTO()
                            {
                                userId = user.id,
                                bookId = getCart.bookId
                            };
                            response += await cartServices.Add(cartDTO, Controller + ".AddCart");
                        }
                        else
                        {
                            if (getCart.bookId.Contains(bookId.ToString()))
                            {
                                response = "book Exist in Cart";
                            }
                            else
                            {
                                getCart.bookId.Add(bookId.ToString());
                                cartDTO = new CartDTO()
                                {
                                    userId = user.id,
                                    bookId = getCart.bookId
                                };
                                response += await cartServices.Add(cartDTO, Controller + ".AddCart");
                            }
                        }
                        getCart = new Cart()
                        {
                            userId = user.id,
                            bookId = getCart.bookId
                        };
                    }
                }
            }
            return response;
        }
        public async Task<string> AddCart(int bookId, string Controller)
        {

            string userDetails = await ConfrimLogin(Controller);
            string response = "";
            CartDTO cartDTO = new CartDTO();
            User user = new User();
            IQueryable<User> usersGet = await userServices.GetAll($"{Controller}.GetBooksInCart");
            if (string.IsNullOrEmpty(Controller) || string.IsNullOrEmpty(userDetails) || bookId < 0)
            {
                response = "Please supply the correct value and user or bookId is not correct";
            }
            else
            {
                user = usersGet.FirstOrDefault(u => (u.email == userDetails) || (u.username == userDetails));

                if (user == null)
                {
                    response = "Please supply the correct value and user does not exist";
                }
                else
                {
                    Cart getCart = await cartServices.Get(user.id, Controller + ".AddCart");
                    if (getCart == null)
                    {
                        List<string> newBooksIdCreate = new List<string>() { bookId.ToString() };
                        cartDTO = new CartDTO()
                        {
                            userId = user.id,
                            bookId = newBooksIdCreate
                        };
                        response = await cartServices.Add(cartDTO, Controller + ".AddCart");
                    }
                    else
                    {
                        if (getCart.bookId.Contains(bookId.ToString()))
                        {
                            response = "book Exist in Cart";
                        }
                        else
                        {
                            getCart.bookId.Add(bookId.ToString());
                            cartDTO = new CartDTO()
                            {
                                userId = user.id,
                                bookId = getCart.bookId
                            };
                            response = await cartServices.Add(cartDTO, Controller + ".AddCart");
                        }
                    }

                }
            }
            return response;
        }

        public async Task<string> AddnewBook(BookDTO bookDTO, string Controller)
        {
            string response = "";
            if (bookDTO == null || string.IsNullOrEmpty(Controller))
            {
                response = "Please supply the correct value and bookId or userId should not not be less than 0";
            }
            else
            {
                IQueryable<Book> getBooks = await bookServices.GetAll(Controller + ".AddBook");
                Book getBook = getBooks.FirstOrDefault(u => u.iSBN == bookDTO.iSBN);
                response = "book Exist";
                if (getBook == null)
                {
                    response = await bookServices.Add(bookDTO, Controller + ".AddnewBook");
                }
            }
            return response;
        }

        public async Task<string> AddUser(UserDTO userDTO, string Controller)
        {
            string response = "";
            if (userDTO == null || string.IsNullOrEmpty(Controller))
            {
                response = "Please supply the correct value, userId should not not be less than 0";
            }
            else
            {
                IQueryable<User> getBooks = await userServices.GetAll(Controller + ".AddUser");
                User getUser = getBooks.FirstOrDefault(u => (u.username == userDTO.username) || (u.email == userDTO.email));
                response = "user Exist";
                if (getUser == null)
                {
                    response = await userServices.Add(userDTO, Controller + ".AddUser");
                }
            }
            return response;
        }

        public async Task<string> DeleteBook(int bookId, string Controller)
        {
            string response = "";
            if (bookId < 0 || string.IsNullOrEmpty(Controller))
            {
                response = "Please supply the correct value, bookId should not not be less than 0";
            }
            else
            {
                Book getBook = await bookServices.GetById(bookId, Controller + ".DeleteBook");
                response = "book does not Exist";
                if (getBook != null)
                {
                    response = await bookServices.Delete(bookId, Controller + ".DeleteBook");
                }

            }
            return response;
        }
        public async Task<Book> GetBook(int bookId, string Controller)
        {
            Book response = new Book();
            if (bookId < 0 || string.IsNullOrEmpty(Controller))
            {
                response = new Book();
            }
            else
            {
                response = await bookServices.GetById(bookId, Controller + ".GetBook");
            }
            return response;
        }

        public async Task<IEnumerable<Book>> GetBooks(string Controller)
        {
            IEnumerable<Book> response = new List<Book>() { null };
            if (string.IsNullOrEmpty(Controller))
            {
                response = new List<Book>();
            }
            else
            {
                IQueryable<Book> response1 = await bookServices.GetAll(Controller + ".GetBooks");
                if (response1.Count() > 0)
                {
                    response = response1.ToList();
                }
            }
            return response;
        }

        public async Task<Cart> GetBooksInCart(string Controller)
        {
            string userDetails = await ConfrimLogin(Controller);
            Cart response = new Cart();
            if (string.IsNullOrEmpty(Controller) || string.IsNullOrEmpty(userDetails))
            {
                response = new Cart();
            }
            else
            {
                IQueryable<User> usersGet = await userServices.GetAll($"{Controller}.GetBooksInCart");
                User user = usersGet.FirstOrDefault(u => (u.email == userDetails) || (u.username == userDetails));
                if (user == null)
                {
                    response = new Cart();
                }
                else
                {
                    response = await cartServices.Get(user.id, Controller + ".GetBooksInCart");
                }
            }
            return response;
        }

        public async Task<Purchase> GetPurchase(string Controller)
        {
            string userDetails = await ConfrimLogin(Controller);
            Purchase response = new Purchase();
            if (string.IsNullOrEmpty(Controller) || string.IsNullOrEmpty(userDetails))
            {
                response = new Purchase();
            }
            else
            {
                IQueryable<User> usersGet = await userServices.GetAll($"{Controller}.GetPurchase");
                User user = usersGet.FirstOrDefault(u => (u.email == userDetails) || (u.username == userDetails));
                if (user == null)
                {
                    response = new Purchase();
                }
                else
                {
                    response = await purchaseServices.GetAllMyPurchase(user.id, Controller + ".GetPurchase");
                }
            }
            return response;
        }

        public async Task<IEnumerable<Purchase>> GetPurchases(string Controller)
        {
            IEnumerable<Purchase> response = new List<Purchase>() { null };
            if (string.IsNullOrEmpty(Controller))
            {
                response = new List<Purchase>();
            }
            else
            {
                IQueryable<Purchase> response1 = await purchaseServices.GetAllOnWeb(Controller + ".GetPurchase");
                if (response1.Count() > 0)
                {
                    response = response1.ToList();
                }
            }
            return response;
        }

        public async Task<User> GetUser(int id, string Controller)
        {
            User response = new User();
            if (string.IsNullOrEmpty(Controller) || id < 0)
            {
                response = new User();
            }
            else
            {
                response = await userServices.GetById(id, Controller + ".GetPurchase");
            }
            return response;
        }

        public async Task<IEnumerable<User>> GetUsers(string Controller)
        {
            IEnumerable<User> response = new List<User>() { null };
            if (string.IsNullOrEmpty(Controller))
            {
                response = new List<User>();
            }
            else
            {
                IQueryable<User> response1 = await userServices.GetAll(Controller + ".GetPurchase");
                if (response1 != null)
                {
                    response = response1.AsEnumerable();
                }
            }
            return response;
        }

        public async Task<string> Login(string userDetails, string Password, string Controller)
        {
            string response = "";
            if (string.IsNullOrEmpty(Controller) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(userDetails))
            {
                response = "pleae supply right data";
            }
            else
            {
                IQueryable<User> getUsers = await userServices.GetAll(Controller + ".Login");
                User getUser = getUsers.FirstOrDefault((u => (u.email == userDetails) || (u.username == userDetails)));
                if (getUser == null)
                {
                    response = "User does not exist";
                }
                else
                {
                    Guid loginStatus = await cacheServices.GetAsync<Guid>("MyUserPassword");
                    if (!loginStatus.Equals(Guid.Empty))
                    {
                        if (loginStatus.Equals(getUser.LoginStatus))
                        {
                            response = "User has been login before";
                        }
                        else
                        {
                            UserDTO userDTO1 = new UserDTO()
                            {
                                id = getUser.id.ToString(),
                                username = getUser.username,
                                password = getUser.password,
                                email = getUser.email,
                                fullName = getUser.fullName,
                                userType = getUser.userType,
                                LoginStatus = Guid.NewGuid()
                            };
                            string response2 = await userServices.Update(userDTO1, getUser.password, Controller + ".Login");
                            if (getUser.password == Password)
                            {
                                await cacheServices.SetAsync<Guid?>("MyUserPassword", getUser.LoginStatus, TimeSpan.FromDays(0.5));
                                await cacheServices.SetAsync<string>("MyUserName", getUser.username, TimeSpan.FromDays(0.5));
                            }
                            {
                                response = "Your Password or your Username or Email is not correct";
                            }
                        }
                    }
                    else
                    {
                        UserDTO userDTO = new UserDTO()
                        {
                            id = getUser.id.ToString(),
                            username = getUser.username,
                            password = getUser.password,
                            email = getUser.email,
                            fullName = getUser.fullName,
                            userType = getUser.userType,
                            LoginStatus = Guid.NewGuid()
                        };
                        string response1 = await userServices.Update(userDTO, getUser.password, Controller + ".Login");
                        if (getUser.password == Password)
                        {
                            await cacheServices.SetAsync<Guid?>("MyUserPassword", getUser.LoginStatus, TimeSpan.FromDays(0.5));
                            await cacheServices.SetAsync<string>("MyUserName", getUser.username, TimeSpan.FromDays(0.5));
                        }
                        {
                            response = "Your Password or your Username or Email is not correct";
                        }
                    }
                }
            }
            return response;
        }
        public async Task<string> ConfrimLogin(string Controller)
        {
            string response = "";
            if (string.IsNullOrEmpty(Controller))
            {
                response = "pleae supply right data";
            }
            else
            {
                IQueryable<User> getUsers = await userServices.GetAll(Controller + ".Login");
                string userDetails = await cacheServices.GetAsync<string>("MyUserName");
                User getUser = getUsers.FirstOrDefault((u => (u.email == userDetails)));
                if (getUser == null)
                {
                    response = "failed";
                }
                else
                {
                    Guid loginStatus = await cacheServices.GetAsync<Guid>("MyUserPassword");
                    if (!loginStatus.Equals(Guid.Empty))
                    {
                        if (loginStatus.Equals(getUser.LoginStatus))
                        {
                            response = getUser.username;
                        }
                        else
                        {
                            await cacheServices.SetAsync<Guid?>("MyUserPassword", Guid.Empty, TimeSpan.FromDays(0.5));
                            await cacheServices.SetAsync<string>("MyUserName", string.Empty, TimeSpan.FromDays(0.5));
                            response = "Failed";
                        }
                    }
                }
            }
            return response;
        }
        public async Task<string> Logout()
        {
            string response = "";
            Guid loginStatus = await cacheServices.GetAsync<Guid>("MyUserPassword");
            if (!loginStatus.Equals(Guid.Empty))
            {
                await cacheServices.SetAsync<Guid>("MyUserPassword", Guid.Empty, TimeSpan.Zero);
                await cacheServices.SetAsync<string>("MyUserName", string.Empty, TimeSpan.Zero);
                response = "Logout Succesfully";
            }
            else
            {
                response = "You must Login Before you will Log out";
            }
            return response;
        }
        public async Task<string> Purchase(string paymentOption, string bookId, string Controller)
        {
            string userDetails = await ConfrimLogin(Controller);
            string response = "";
            PurchaseDTO purchaseDTO = new PurchaseDTO();
            User user = new User();
            Book book = new Book();
            IQueryable<User> usersGet = await userServices.GetAll($"{Controller}.Purchase");
            if (string.IsNullOrEmpty(Controller) || string.IsNullOrEmpty(userDetails) || string.IsNullOrEmpty(paymentOption) || string.IsNullOrEmpty(bookId))
            {
                response = "Please supply the correct value and user or bookId is not correct";
            }
            else
            {
                user = usersGet.FirstOrDefault(u => (u.email == userDetails) || (u.username == userDetails));
                book = await bookServices.GetById(int.Parse(bookId), $"{Controller}.Purchase");
                if (user == null || book == null)
                {
                    response = "Please supply the correct value and book or user does not exist";
                }
                else
                {
                    Purchase getpurchase = await purchaseServices.GetAllMyPurchase(user.id, Controller + ".Purchase");
                    if (getpurchase == null)
                    {
                        List<string> newBooksIdCreate = new List<string>() { bookId };
                        purchaseDTO = new PurchaseDTO()
                        {
                            userId = user.id,
                            booksId = newBooksIdCreate
                        };
                        response = await purchaseServices.Add(purchaseDTO, Controller + ".Purchase");
                    }
                    else
                    {
                        if (getpurchase.booksId.Contains(bookId))
                        {
                            response = "book Exist in Cart";
                        }
                        else
                        {
                            getpurchase.booksId.Add(bookId);
                            purchaseDTO = new PurchaseDTO()
                            {
                                userId = user.id,
                                booksId = getpurchase.booksId
                            };
                            response = await purchaseServices.Update(purchaseDTO, Controller + ".Purchase");
                        }
                    }

                }
            }
            return response;
        }

        public async Task<string> Purchase(CartDTO cartDTOs, string Controller)
        {
            string response = "";
            CartDTO cartDTO = new CartDTO();
            PurchaseDTO purchaseDTO = new PurchaseDTO();
            User user = new User();
            Cart cart = new Cart();
            IQueryable<User> usersGet = await userServices.GetAll($"{Controller}.Purchase");
            if (string.IsNullOrEmpty(Controller) || cartDTOs == null)
            {
                response = "Please supply the correct value and user or bookId is not correct";
            }
            else
            {
                cart = await cartServices.Get(cartDTO.userId, $"{Controller}.Purchase");
                if (user == null || cart == null)
                {
                    response = "Their is nothing in cart to buy or user does not exist";
                }
                else
                {
                    Purchase getpurchase = await purchaseServices.GetAllMyPurchase(user.id, Controller + ".Purchase");
                    if (getpurchase == null)
                    {
                        purchaseDTO = new PurchaseDTO()
                        {
                            userId = user.id,
                            booksId = cartDTO.bookId
                        };
                        response = await purchaseServices.Add(purchaseDTO, Controller + ".Purchase");
                        if (response == "Successful")
                        {
                            response = await cartServices.Delete(cartDTO.userId, Controller + ".Purchase");
                        }
                    }
                    else
                    {
                        foreach (string bookkId in cartDTO.bookId)
                        {
                            if (getpurchase.booksId.Contains(cartDTO.bookId.ToString()))
                            {
                                response += $"{bookkId} book Exist in purchase  ||  ";
                            }
                            else
                            {
                                getpurchase.booksId.Add(bookkId);
                                purchaseDTO = new PurchaseDTO()
                                {
                                    userId = user.id,
                                    booksId = getpurchase.booksId
                                };
                                response += $"{bookkId} book is now purchased ||  ";
                            }
                        }
                        string request = await purchaseServices.Update(purchaseDTO, Controller + ".Purchase");
                        if (request == "cart Updated Succesfully")
                        {
                            return response;
                        }
                        else
                        {
                            response = "no book is added";
                        }
                    }

                }
            }
            return response;
        }

        public async Task<string> RemoveFromCart(int id, string Controller)
        {
            string response = "";
            if (id < 0 || string.IsNullOrEmpty(Controller))
            {
                response = "Please supply the correct value, bookId should not not be less than 0";
            }
            else
            {
                Book getBook = await bookServices.GetById(id, Controller + ".DeleteBook");
                response = "book does not Exist";
                if (getBook != null)
                {
                    response = await bookServices.Delete(id, Controller + ".DeleteBook");
                }

            }
            return response;
        }

        public async Task<IEnumerable<Book>> SearchBook(string parameter, string Controller)
        {
            List<Book> books = new List<Book>();
            if (string.IsNullOrEmpty(parameter) || string.IsNullOrEmpty(Controller))
            {
                return books;
            }
            else
            {
                IEnumerable<Book> booksGet = (await bookServices.GetAll($"{Controller}.GetPurchase")).ToList();
                if (booksGet != null)
                {
                    return books;
                }
                books = booksGet.Where(u => (u.iSBN == parameter) || (u.title == parameter) || (u.title == parameter) || (u.price == double.Parse(parameter))).ToList();
                return books;
            }
        }
        public async Task<string> UpdateBook(BookDTO bookDTO, string Controller)
        {
            string response = "";
            if (string.IsNullOrEmpty(Controller) || bookDTO == null)
            {
                response = "Please supply the correct value and user or bookId is not correct";
            }
            else
            {
                Book book = await bookServices.GetById(int.Parse(bookDTO.id), Controller + "UpdateBook");
                response = "book does not exist";
                if (book != null)
                {
                    response = await bookServices.Update(bookDTO, Controller);
                }
            }
            return response;
        }

        public async Task<string> UpdateCart(CartDTO cartDTO, string Controller)
        {
            {
                string response = "";
                if (string.IsNullOrEmpty(Controller) || cartDTO == null)
                {
                    response = "Please supply the correct value and user or bookId is not correct";
                }
                else
                {
                    Cart cart = await cartServices.Get(cartDTO.userId, Controller + "UpdateBook");
                    response = "book does not exist";
                    if (cart != null)
                    {
                        response = await cartServices.Update(cartDTO, Controller);
                    }
                }
                return response;
            }
        }
        public async Task<string> UpdateUser(UserDTO userDTO, string oldPasword, string Controller)
        {
            string response = "";
            if (userDTO == null || string.IsNullOrEmpty(Controller))
            {
                response = "Please supply the correct value, userId should not not be less than 0";
            }
            else
            {
                IQueryable<User> getUsers = await userServices.GetAll(Controller + ".AddUser");
                User getUser = getUsers.FirstOrDefault(u => (u.username == userDTO.username) || (u.email == userDTO.email));
                response = "user Does Not Exist";
                if (getUser != null)
                {
                    response = await userServices.Update(userDTO, oldPasword, Controller + ".AddUser");
                }
            }
            return response;
        }
        //public async Task<string> AddMoney(string userDetails,decimal MoneytoAdd, string Controller)
        //{
        //    string response = "";
        //    if ( string.IsNullOrEmpty(Controller)||string.IsNullOrEmpty(userDetails))
        //    {
        //        response = "Please supply the correct value, userId should not not be less than 0";
        //    }
        //    else
        //    {
        //        IQueryable<User> getUsers = await userServices.GetAll(Controller + ".AddUser");
        //        User getUser = getUsers.FirstOrDefault(u => (u.username == userDetails) || (u.email == userDetails));
        //        response = "user Does Not Exist";
        //        if (getUser != null)
        //        {
        //            UserDTO userDTO = new UserDTO()
        //            {
        //                id = getUser.id.ToString(),
        //                username = getUser.username,
        //                balance = getUser.balance+ MoneytoAdd,
        //                LoginStatus = getUser.LoginStatus,
        //                password = getUser.password,
        //                email = getUser.email,
        //                fullName = getUser.fullName,
        //                userType = getUser.userType,
        //            };
        //            response = await userServices.Update(userDTO, getUser.password, Controller + ".AddUser");
        //        }
        //    }
        //    return response;
        //}
        //public async Task<string> DeleteUser(int id, string Controller)
        //{
        //    string response = "";
        //    if (id < 0 || string.IsNullOrEmpty(Controller))
        //    {
        //        response = "Please supply the correct value, bookId should not not be less than 0";
        //    }
        //    else
        //    {
        //        User getUser = await userServices.GetById(id, Controller + ".DeleteUser");
        //        response = "user does not Exist";
        //        if (getUser != null)
        //        {
        //            response = await userServices.De(id, Controller + ".DeleteUser");
        //        }

        //    }
        //    return response;
        //}

    }
}