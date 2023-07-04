using Authentication.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Services.Shared.Models;

namespace Authentication.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string URL = $"http://localhost:7000/api/Database";
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Login(string username, string password)
        {
            HttpClient client = new HttpClient();
            string fullURL = URL + $"/GetUserForAuthentication?username={username}&password={password}"; 
            //string fullURL = URL + $"/GetUser?username=kadirtuna&password=12345"; 
            var responseTask = client.GetAsync(fullURL);
            responseTask.Wait();
            String message = null;

            if (responseTask.IsCompleted)
            {
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var messageTask = result.Content.ReadAsStringAsync();
                    messageTask.Wait();

                    message = messageTask.Result;
                }
            }

            if (message != null)
            {
                User user = JsonConvert.DeserializeObject<User>(message);

                if(user.State == true)
                    return await CreateToken(user);
            }

            return null;
        }

        //public User GetUserByUsername(string username)
        //{
        //    var query = $"SELECT * FROM Users WHERE Username = '{username}'";
        //    User user = _dataContext.Users.FromSqlRaw(query).FirstOrDefault();

        //    if (user == null)
        //        return null;
        //    else
        //        return user;
        //}

        public async Task<string> CreateToken(User user)
        {
            var bytes = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            //var bytes = Encoding.UTF8.GetBytes("IT'S A TRIAL JWT TOKEN INTEGRATION");
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(issuer: _configuration.GetSection("Jwt:Issuer").Value, audience: _configuration.GetSection("Jwt:Issuer").Value, notBefore: DateTime.Now, claims: claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            //JwtSecurityToken token = new JwtSecurityToken(issuer: "https://localhost", audience: "https://localhost", notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
