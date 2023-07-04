using System.Text;
using System;
using System.Diagnostics.CodeAnalysis;
using Services.Shared.DTO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApp.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private const string URL = $"https://localhost:7225/api/Auth/Login";
        public ApiClient()
        {
            this._httpClient = new HttpClient();
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            AuthenticationDTO authenticationDTO = new AuthenticationDTO()
            {
                Username = username,
                Password = password
            };
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(authenticationDTO), Encoding.UTF8, "application/json" );
            var responseTask = _httpClient.PostAsync(URL, stringContent);
            responseTask.Wait();
            string message = null;

            if (responseTask.IsCompletedSuccessfully)
            {
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var messageTask = result.Content.ReadAsStringAsync();
                    message = messageTask.Result;

                    var jToken = JsonConvert.DeserializeObject<JToken>(message);
                    var token = jToken["token"];

                    return (string) token;
                }
            }

            return null;
        }
    }
}
















//HttpClient client = new HttpClient();
//string fullURL = URL + $"PutUser";
//var content = new StringContent(JsonConvert.SerializeObject(updateUserDTO), Encoding.UTF8, "application/json");
//var responseTask = client.PutAsync(fullURL, content);
//responseTask.Wait();

//return await responseTask;