using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Services.Shared.DTO;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using static WebAppBlazor.Pages.Login;

namespace WebAppBlazor.ClientServices
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private const string URL = $"https://localhost:7225/api/Auth";

        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> RegistToken(AuthenticationDTO authenticationDTO)
        {
            
            string fullURL = URL + $"/Login";
            var responseTask = await _httpClient.PostAsJsonAsync(fullURL, authenticationDTO);
            var result = await responseTask.Content.ReadAsStringAsync();
            string token = null;

            if (responseTask.IsSuccessStatusCode)
            {
                var tokenDTO = JsonConvert.DeserializeObject<TokenDTO>(result);
                token = tokenDTO.Token;
                await _localStorage.SetItemAsync("token", token.Replace("\"", ""));
                await _authenticationStateProvider.GetAuthenticationStateAsync();

                return true;
            }
            
            return false;
        }
    }
}
