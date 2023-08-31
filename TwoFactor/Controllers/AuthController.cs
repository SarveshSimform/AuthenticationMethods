using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwoFactor.Models;
using TwoFactor.Services;

namespace TwoFactor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public AuthController(TokenService tokenSerivice)
        {
            _tokenService = tokenSerivice;
        }
        [HttpPost("login")]
        public IActionResult Login(UserModel model)
        {
            // Perform user authentication here
            // For simplicity, we'll use a hardcoded username and password
            if (model.Username == "testuser" && model.Password == "password")
            {
                bool isTwoFactorEnabled = true; // Assume that 2FA is enabled for this user
                var token = _tokenService.GenerateToken(model.Username, isTwoFactorEnabled);
                return Ok(new TokenModel { Token = token });
            }

            return BadRequest("Invalid credentials.");
        }

        [HttpGet("protected")]
        [Authorize]
        public IActionResult Protected()
        {
            return Ok("Welcome! This is a protected endpoint.");
        }
    }
}
