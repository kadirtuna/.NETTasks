using Services.Shared.DTO;
using Services.Shared.Models;

namespace WebAppBlazor.ClientServices
{
    public interface IUserService
    {
        public Task<User> ReadUser();
        public Task<List<User>> ReadAllUsers();
        public Task<string> Register(CreateUserDTO createUserDTO);
        public Task<string> UpdateUser(UpdateUserDTO updateUserDTO);
        public Task<string> DeleteUser(DeleteUserDTO deleteUserDTO);
    }
}
