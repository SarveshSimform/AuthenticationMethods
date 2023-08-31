using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TokenBased.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpGet("fetch")]
        public IActionResult Fetch()
        {
            var data = new List<string>()
            {
                "Monday","Tuesday","Wednesday","Thrusday","Friday"
            };
            return Ok(data);
        }
    }
}
