using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace RateLimit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Basic")]
    public class ValuesController : ControllerBase
    {
        public static int _employeesNumber;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_employeesNumber);
        }

        [HttpPost]
        public async Task<IActionResult> Set(int employeesNumber)
        {
            _employeesNumber = employeesNumber;

            return Ok(_employeesNumber);
        }
    }
}
