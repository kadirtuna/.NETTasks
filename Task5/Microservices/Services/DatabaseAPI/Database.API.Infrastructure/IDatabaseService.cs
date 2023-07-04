using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Shared.Models;

namespace Database.API.Infrastructure
{
    public interface IDatabaseService
    {
        public Task<User> GetUser(String username, String password);
        public Task<User> GetUserByUsername(string username);
    }
}
