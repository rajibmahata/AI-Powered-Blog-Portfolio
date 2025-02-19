using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AIPoweredBlogPortfolio.Admin.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        private readonly ILocalStorageService _localStorageService;
        private bool _isInitialized;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!_isInitialized)
            {
                return anonymous;
            }

            var savedUsername = await _localStorageService.GetItemAsync<string>("username");

            if (string.IsNullOrEmpty(savedUsername))
            {
                return anonymous;
            }

            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, savedUsername) }, "apiauth"));
            return new AuthenticationState(authenticatedUser);
        }

        public async Task InitializeAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
            _isInitialized = true;
        }

        public async Task MarkUserAsAuthenticated(string username)
        {
            await _localStorageService.SetItemAsync("username", username);
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("username");
            var authState = Task.FromResult(anonymous);
            NotifyAuthenticationStateChanged(authState);
        }

        public Task UpdateAuthenticationState(ClaimsPrincipal user)
        {
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
            return Task.CompletedTask;
        }
    }
}
