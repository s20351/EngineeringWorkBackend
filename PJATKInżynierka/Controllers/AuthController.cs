using Domain.DTOs.AuthDTOs;
using Domain.DTOs.RegisterDTOs;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthDatabaseService _dbService;

        public AuthController(IAuthDatabaseService dbService)
        {
            _dbService = dbService;
        }   

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            _dbService.Create(registerDTO);
            return StatusCode(200, "User registered");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var login = _dbService.Login(loginDTO);
            if (login == null)
            {
                return BadRequest(new { message = "Wrong Email or Password" });
            }
            else
            {
                return Ok(login.FarmerId);
            }
        }
    }
}
