using Newtonsoft.Json;
using Services.Shared.DTO;
using Services.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace WebAppBlazor.ClientServices
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly string clientURL = "https://localhost:7225/api/ManageUser";

        public UserService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorageService;   
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<User> ReadUser()
        {
            string username = await GetUsernameOfCurrentUser();
            string fullURL = clientURL + $"/GetUserByUsername/{username}";
            var token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));

            var resultTask = await _httpClient.GetAsync(fullURL);
            User user = new User();
            
            if (resultTask.IsSuccessStatusCode)
            {
                var message = await resultTask.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(message);

                return user;
            }

            return user;
        }

        public async Task<List<User>> ReadAllUsers()
        {
            string fullURL = clientURL + $"/GetAllUsers";
            var token = await _localStorage.GetItemAsStringAsync("token");

            if (!String.IsNullOrEmpty(token)){
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            }

            var resultTask = await _httpClient.GetAsync(fullURL);
            List<User> users = new List<User>();

            if (resultTask.IsSuccessStatusCode)
            {
                var message = await resultTask.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(message);

                return users;
            }

            return users;
        }

        public async Task<string> Register(CreateUserDTO createUserDTO)
        {
            string fullURL = clientURL + $"/PostUser";
            var resultTask = await _httpClient.PostAsJsonAsync<CreateUserDTO>(fullURL, createUserDTO);

            if (resultTask.IsSuccessStatusCode)
            {
                var message = await resultTask.Content.ReadAsStringAsync();

                return "The user has been succesfully added!";
            }
            else if (resultTask.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "This username has already been taken!";
            }

            return "There is an error in server-side. Try again later!";
        }

        public async Task<string> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            string fullURL = clientURL + $"/PutUser";
            var token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));

            var resultTask = await _httpClient.PutAsJsonAsync<UpdateUserDTO>(fullURL, updateUserDTO);

            if (resultTask.IsSuccessStatusCode)
            {
                var message = await resultTask.Content.ReadAsStringAsync();

                return "The user has been succesfully updated!";
            }
            else if (resultTask.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "ERROR: The user couldn't be updated!";
            }

            return "There is an error in server-side. Try again later!";
        }

        public async Task<string> DeleteUser(DeleteUserDTO deleteUserDTO)
        {
            string fullURL = clientURL + $"/DeleteUser?username={deleteUserDTO.Username}";
            var token = await _localStorage.GetItemAsStringAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));

            var resultTask = await _httpClient.PutAsJsonAsync<DeleteUserDTO>(fullURL, deleteUserDTO);

            if (resultTask.IsSuccessStatusCode)
            {
                var message = await resultTask.Content.ReadAsStringAsync();

                await _localStorage.RemoveItemAsync("token");
                await _authenticationStateProvider.GetAuthenticationStateAsync();
                return "The user has been succesfully inactivated!";
            }
            else if (resultTask.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "ERROR: The user couldn't be inactivated!";
            }

            return "There is an error in server-side. Try again later!";
        }

        private async Task<string> GetUsernameOfCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                return user.Identity.Name;
            }

            return null;
        }

    }
}
