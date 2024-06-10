using OnlineBookStore_Ass.Domain.DTO;
using OnlineBookStore_Ass.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.UtilityServices.General
{
    public class Mapper
    {
        public static BookDTO InputMapperParameterToBookDTO( string id,string title, string? description, string author, string iSBN, string publicationYear, double price)
        {
            if (id == null)
            {
                return new BookDTO()
                {
                    title = title,
                    description = description,
                    author = author,
                    iSBN = iSBN,
                    publicationYear = publicationYear,
                    price = price
                };
            }
            return new BookDTO()
            {
                id=id,
                title=title,
                description=description,
                author=author,
                iSBN=iSBN,
                publicationYear=publicationYear,
                price=price
            };
        }
        public static UserDTO InputMapperParameterToUserDTO( string username, string? password, string? email, string? fullName, Usertype userType)
        {
            return new UserDTO()
            {
                username=username,
                password=password,
                email=email,
                fullName=fullName,
                userType=userType
            };
        }
        //public static UserDTO InputMapperParameterToCartDTO(         public int userId { get; set; }
        //public List<string> bookId)
        //{
        //    return new UserDTO()
        //    {
        //        username=username,
        //        password=password,
        //        email=email,
        //        fullName=fullName,
        //        userType=userType
        //    };
        //}
    }
}
