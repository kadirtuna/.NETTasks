using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.Shared.DTO;
using Services.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UserManagement.API.Infrastructure;

namespace UserManagement.API.Services
{
    public class UserManagementService : IUserManagementService
    {
        private const string URL = $"http://localhost:7000/api/Database/";
        private readonly IConfiguration _configuration;

        public UserManagementService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> Insert(CreateUserDTO createUserDTO)
        {
            HttpClient client = new HttpClient();
            string fullURL = URL + $"PostUser";
            var content = new StringContent(JsonConvert.SerializeObject(createUserDTO), Encoding.UTF8, "application/json");
            var responseTask = client.PostAsync(fullURL, content);
            responseTask.Wait();

            return await responseTask;
        }
        public async Task<HttpResponseMessage> Update(UpdateUserDTO updateUserDTO)
        {
            HttpClient client = new HttpClient();
            string fullURL = URL + $"PutUser";
            var content = new StringContent(JsonConvert.SerializeObject(updateUserDTO), Encoding.UTF8, "application/json");
            var responseTask = client.PutAsync(fullURL, content);
            responseTask.Wait();

            return await responseTask;
        }

        public async Task<HttpResponseMessage> Delete(string username)
        {
            HttpClient client = new HttpClient();
            string fullURL = URL + $"DeleteUser?username={username}";
            var content = new StringContent(JsonConvert.SerializeObject(username), Encoding.UTF8, "application/json");
            var responseTask = client.PutAsync(fullURL, content);
            responseTask.Wait();

            return await responseTask;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            HttpClient client = new HttpClient();
            string fullURL = URL + $"GetAllUsers";
            var responseTask = client.GetAsync(fullURL);
            responseTask.Wait();
            string message = null;

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
                IEnumerable<User> userList = JsonConvert.DeserializeObject<IEnumerable<User>>(message);
                return userList;
            }

            return null;
        }

        public async Task<User> GetUserById(int Id) //Rest Driven Architecture
        {
            HttpClient client = new HttpClient();
            string fullURL = URL + $"GetUserById/{Id}";
            var responseTask = client.GetAsync(fullURL);
            responseTask.Wait();
            string message = null;

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
                return user;
            }

            return null;
        }

        public async Task<User> GetUserByUsername(string username) //Rest Driven Architecture
        {
            HttpClient client = new HttpClient();
            string fullURL = URL + $"GetUserByUsername/{username}";
            var responseTask = client.GetAsync(fullURL);
            responseTask.Wait();
            string message = null;

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
                return user;
            }

            return null;
        }

    }
}
