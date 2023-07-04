using Database.API.Infrastructure;
using Database.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Services.Shared.Models;

namespace Database.API.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;

        public DatabaseService(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }

        public async Task<User> GetUser(string username, string password)
        {
            var query = $"SELECT * FROM Users WHERE Username = '{username}' and Password = '{password}'";
            User user= _dataContext.Users.FromSqlRaw(query).FirstOrDefault();

            if (user == null)
                return null;
            else
                return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var query = $"SELECT * FROM Users WHERE Username = '{username}'";
            User user = _dataContext.Users.FromSqlRaw(query).FirstOrDefault();

            if (user == null)
                return null;
            else
                return user;
        }
    }
}
