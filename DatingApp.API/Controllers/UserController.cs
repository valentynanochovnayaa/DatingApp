using System.Collections.Generic;
using DatingApp.API.Models;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<UserForMongo>> GetUsers()
        {
            return _userService.Get();
        }
        [HttpGet("{id}", Name="User")]
        public ActionResult<UserForMongo> GetUser(string id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpPost]
        public ActionResult<UserForMongo> CreateUser(UserForMongo user)
        {
            _userService.Create(user);
            return CreatedAtRoute("User", new { id = user.Id.ToString()}, user);
        }
        [HttpDelete("{id}")]
        public ActionResult<UserForMongo> DeleteUser(string id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();

            }
            _userService.Remove(user);
            return NoContent();
        }
        
    }
}