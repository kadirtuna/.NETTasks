using Authentication.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Shared.DTO;
using System.Security.Claims;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService) 
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(AuthenticationDTO authenticationDTO)
        {
            var generatedToken = await _authenticationService.Login(authenticationDTO.Username, authenticationDTO.Password);

            if (generatedToken != null)
                return Ok(new {Token = generatedToken });
            else
                return BadRequest("Given username or password is incorrect.");
        }

        [HttpGet("Trial")]
        [Authorize]
        public async Task<ActionResult> GetValue()
        {
            return Ok("Congrutulations!");
        }

        //[HttpGet("{Id}")]
        //public AuthenticationDTO Get(int Id)
        //{
        //    string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault(h => h.StartsWith("Bearer "));

        //    if (!string.IsNullOrEmpty(token) ) 
        //    { 
        //        token = token.Substring("Bearer ".Length).Trim();
        //        Console.WriteLine(token);
        //    }
        //    else
        //        Console.WriteLine("Token is null!");


        //    return _authenticationService.GetAuthenticationById(Id, token);
        //}

        //public string GetTokenOfCurrentUser()
        //{
        //    string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault(h => h.StartsWith("Bearer"));

        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        token = token.Substring("Bearer ".Length).Trim();

        //        return token;
        //    }
        //    else
        //        return null;

        //}
    }
}
