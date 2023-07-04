using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Shared.DTO;
using Services.Shared.Models;
using UserManagement.API.Infrastructure;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpGet("GetUserById/{Id}")]
        [Authorize]
        public async Task<ActionResult> GetUserById(int Id)
        {
            var user = await _userManagementService.GetUserById(Id);

            if (user != null)
                return Ok(user);
            else
                return NotFound("Any user by given id couldn't be found!");
        }

        [HttpGet("GetUserByUsername/{username}")]
        [Authorize]
        public async Task<ActionResult> GetUserByUsername(string username)
        {
            var user = await _userManagementService.GetUserByUsername(username);

            if (user != null)
                return Ok(user);
            else
                return NotFound("Any user by given id couldn't be found!");
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userManagementService.GetAll();

            if (users != null)
                return Ok(users);
            else
                return BadRequest("Any user couldn't be found in the database!");
        }

        [HttpPost("PostUser")]
        public async Task<ActionResult> InsertUser(CreateUserDTO createUserDTO)
        {
            var response = _userManagementService.Insert(createUserDTO);

            if (response.IsCompletedSuccessfully)
            {
                if (response.Result.IsSuccessStatusCode)
                {
                    return Ok($"The user {createUserDTO.Username} has been succesfully added to the database!");
                }
                else
                    return BadRequest("The user couldn't be added to the database!");

            }

            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }

        [HttpPut("PutUser")]
        public async Task<ActionResult<string>> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            var response = _userManagementService.Update(updateUserDTO);

            if (response.IsCompletedSuccessfully)
            {
                if (response.Result.IsSuccessStatusCode)
                {
                    return Ok($"The user {updateUserDTO.Username} has been succesfully updated in the database!");
                }
                else
                    return BadRequest("The user couldn't be updated in the database!");

            }

            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }

        [HttpPut("DeleteUser")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            var response = _userManagementService.Delete(username);

            if (response.IsCompletedSuccessfully)
            {
                if (response.Result.IsSuccessStatusCode)
                {
                    return Ok($"The state of \"{username}\" has been succesfully changed to \"Passive\"!");
                }
                else
                    return BadRequest("The user couldn't be deleted in the database!");

            }

            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }

    }
}
