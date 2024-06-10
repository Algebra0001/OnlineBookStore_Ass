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
    public class UserController : ControllerBase
    {
        private readonly IOnlineBookStoreMainServices _onlineBookStoreMainServices;
        private readonly string _controller;
        public UserController(IOnlineBookStoreMainServices onlineBookStoreMainServices) 
        {
            _onlineBookStoreMainServices = onlineBookStoreMainServices;
            _controller = "UserController";
        }

        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddUser(string username,string password,string email,string fullName,Usertype userType)
        {
            UserDTO userDTO = Mapper.InputMapperParameterToUserDTO(username, password, email, fullName, userType);
            string response = await _onlineBookStoreMainServices.AddUser(userDTO, _controller);
            return Ok(Response);
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(int userId)
        {
            User response = await _onlineBookStoreMainServices.GetUser(userId, _controller);
            return Ok(Response);
        }
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<User> response = await _onlineBookStoreMainServices.GetUsers( _controller);
            return Ok(Response);
        }
        [HttpGet("Login")]
        public async Task<IActionResult> Login(string userDetails, string password)
        { 

            string response = await _onlineBookStoreMainServices.Login( userDetails, password, _controller);
            return Ok(Response);
        }

        [HttpDelete("Logout")]
        public async Task<IActionResult> LogOut()
        { 

            string response = await _onlineBookStoreMainServices.Logout();
            return Ok(Response);
        }
        [HttpPut("UpdateUserByAdmin")]
        public async Task<IActionResult> UpdateUser(string username, string? password, string? email, string? fullName, Usertype userType)
        {
            UserDTO userDTO = Mapper.InputMapperParameterToUserDTO(username, password, email, fullName, userType);
            string response = await _onlineBookStoreMainServices.UpdateUser(userDTO,password,_controller);
            return Ok(Response);
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(string? password, string? email, string? fullName, Usertype userType)
        {
            string username = await _onlineBookStoreMainServices.ConfrimLogin(_controller);
            if(username== "pleae supply right data"||username== "Failed")
            {
                return Ok("Please Login before you will be able to handle this");
            }
            UserDTO userDTO = Mapper.InputMapperParameterToUserDTO(username, password, email, fullName, userType);
            string response = await _onlineBookStoreMainServices.UpdateUser(userDTO,password,_controller);
            return Ok(Response);
        }
    //    string username = await _onlineBookStoreMainServices.ConfrimLogin(_controller);
    //    string response = "";
    //        if (username == null)
    //        {
    //            response = await _onlineBookStoreMainServices.Login(username, password, _controller);
    //}
    //        else
    //        {
    //            response="user has already login Succesfully"
    //        }
    }
}
