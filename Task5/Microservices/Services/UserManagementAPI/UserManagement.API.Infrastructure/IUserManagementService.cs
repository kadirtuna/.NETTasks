using Services.Shared.DTO;
using Services.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.API.Infrastructure
{
    public interface IUserManagementService
    {
        Task<HttpResponseMessage> Insert(CreateUserDTO createUserDTO);
        Task<HttpResponseMessage> Update(UpdateUserDTO updateUserDTO);
        Task<HttpResponseMessage> Delete(string username);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
    }
}
