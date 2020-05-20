using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForREgisterDto)
        {
            //validate request

            userForREgisterDto.Username = userForREgisterDto.Username.ToLower();
            if(await _repo.UserExists(userForREgisterDto.Username))
                return BadRequest("Username already exists");
            
            var userToCreate = new User
            {
                Username = userForREgisterDto.Username
            };
            var createdUser = await _repo.Register(userToCreate, userForREgisterDto.Password);
            return StatusCode(201);
        }
    }
}