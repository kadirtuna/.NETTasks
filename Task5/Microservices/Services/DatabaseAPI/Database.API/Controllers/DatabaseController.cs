using Database.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Services.Shared.Models;
using Services.Shared.DTO;

namespace Database.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IRepositoryService<User> _userRepository;
        private readonly IDatabaseService _databaseService;

        public DatabaseController(IRepositoryService<User> userRepository, IDatabaseService loginService)
        {
            _userRepository = userRepository;
            _databaseService = loginService;
        }

        [HttpGet("GetUserForAuthentication")]
        public async Task<ActionResult> GetUserAsync(String username, String password)
        {
            User user = await _databaseService.GetUser(username, password);

            if (user != null)
                return Ok(user);
            else
                return Unauthorized("Any user couldn't be found by given username or password!");
        }

        [HttpGet("GetUserById/{Id:int}")]
        public async Task<ActionResult> GetUserById(int Id) 
        {
            var user = await _userRepository.GetById(Id);

            if (user != null)
                return Ok(user);
            else
                return BadRequest("A user couldn't be found with given Id!");
        }

        [HttpGet("GetUserByUsername/{username}")]
        public async Task<ActionResult> GetUserByUsername(string username)
        {
            var user = await _databaseService.GetUserByUsername(username);

            if (user != null)
                return Ok(user);
            else
                return BadRequest("A user couldn't be found with given Id!");
        }

        [HttpGet("GetAllUsers")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAll();

            if (users != null)
                return Ok(users);   
            else
                return BadRequest("A user couldn't be found in the database!");
        }

        [HttpPost("PostUser")]
        public async Task<ActionResult> InsertUser(CreateUserDTO createUserDTO)
        {
            User userControl = await _databaseService.GetUserByUsername(createUserDTO.Username);

            if (userControl != null)
                return BadRequest("A user has already registered by given username!");

            User user = new User(){

                Name = createUserDTO.Name,
                Surname = createUserDTO.Surname,
                Username = createUserDTO.Username,
                Password = createUserDTO.Password,
                Role = "User",
                Email = createUserDTO.Email,
                Phone = createUserDTO.Phone,
                State = true
            };

            await _userRepository.Insert(user);
            await _userRepository.CommitAsync();

            return Ok(String.Format("The user with \"{0}\" has been successfully added to the database!", user.Username));
        }

        [HttpPut("PutUser")]
        public async Task<ActionResult<string>> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            var user = await _databaseService.GetUserByUsername(updateUserDTO.Username);   

            if (user == null)
            {
                return NotFound("Any user couldn't be found with given Id!");
            }

            user.Name = updateUserDTO.Name;
            user.Surname = updateUserDTO.Surname;
            user.Username = updateUserDTO.Username;
            user.Password = updateUserDTO.Password;
            user.State = updateUserDTO.State;
            user.Email = updateUserDTO.Email;
            user.Phone = updateUserDTO.Phone;

            await _userRepository.Update(user);
            await _userRepository.CommitAsync();

            return Ok(String.Format("The user with {0} has been succesfully updated!", user.Username));
        }

        [HttpPut("DeleteUser")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            var user = await _databaseService.GetUserByUsername(username);

            if (user == null)
            {
                return NotFound("Any user couldn't be found with given username!");
            }

            user.State = false;

            await _userRepository.Update(user);
            await _userRepository.CommitAsync();

            return Ok($"The state of \"{user.Username}\" has been succesfully changed to \"Passive\"!");
        }
    }
}
