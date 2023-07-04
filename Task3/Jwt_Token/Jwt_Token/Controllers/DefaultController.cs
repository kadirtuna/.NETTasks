using Jwt_Token.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jwt_Token.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        readonly IConfiguration _configuration;

        public DefaultController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return Created("", new BuildToken(_configuration).CreateToken());
        }

        [Authorize]
        [HttpGet("action")]
        public IActionResult Page1()
        {
            return Ok("You are succesfully entered in!");
        }

    }
}
