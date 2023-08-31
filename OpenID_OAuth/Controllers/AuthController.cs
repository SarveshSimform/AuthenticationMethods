using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;

namespace OpenID_OAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("protected")]
        [Authorize]
        public IActionResult Protected()
        {
            var user = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok($"Hello, {user}! This is a protected endpoint.");
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            // Redirect to the OpenID Connect authentication endpoint
            return Challenge(new AuthenticationProperties { RedirectUri = "/api/auth/callback" });
        }

        [HttpGet("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // Redirect to the OpenID Connect logout endpoint
            return SignOut(new AuthenticationProperties { RedirectUri = "/api/auth/callback" }, JwtBearerDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet("callback")]
        [AllowAnonymous]
        public IActionResult Callback()
        {
            // Handle the OpenID Connect callback
            return Ok("Callback endpoint");
        }
    }
}