using Blazored.LocalStorage;
using FunTrophy.Shared.Model.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FunTrophy.Web.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(
            HttpClient client,
            AuthenticationStateProvider authStateProvider,
            ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<Token?> Login(AuthenticationUser userForAuthentication)
        {
            var data = new AuthenticationUser
            {
                UserName = userForAuthentication.UserName,
                Password = userForAuthentication.Password,
            };
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var authResult = await _client.PostAsync("User/Authenticate", content);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }
            var result = JsonSerializer.Deserialize<Token>(
                authContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await _localStorage.SetItemAsync("authToken", result!.AccessToken);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication();

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication();
        }
    }
}
