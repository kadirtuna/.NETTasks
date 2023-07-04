using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Shared;

namespace Authentication.API.Infrastructure
{
    public interface IAuthenticationService
    {
        //public AuthenticationDTO GetAuthenticationById(int Id, string token);
        public Task<string> Login(string username, string password);
        //public User GetUserByUsername(string username);

    }
}
