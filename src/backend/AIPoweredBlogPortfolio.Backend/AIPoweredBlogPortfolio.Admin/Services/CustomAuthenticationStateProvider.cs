using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AIPoweredBlogPortfolio.Admin.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        private bool _isInitialized;
        private string _savedUsername;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!_isInitialized)
            {
                return Task.FromResult(anonymous);
            }

            if (string.IsNullOrEmpty(_savedUsername))
            {
                return Task.FromResult(anonymous);
            }

            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, _savedUsername) }, "apiauth"));
            return Task.FromResult(new AuthenticationState(authenticatedUser));
        }

        public Task InitializeAsync()
        {
            var authState = GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(authState);
            _isInitialized = true;
            return Task.CompletedTask;
        }

        public Task MarkUserAsAuthenticated(string username)
        {
            _savedUsername = username;
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
            return Task.CompletedTask;
        }

        public Task MarkUserAsLoggedOut()
        {
            _savedUsername = null;
            var authState = Task.FromResult(anonymous);
            NotifyAuthenticationStateChanged(authState);
            return Task.CompletedTask;
        }

        public Task UpdateAuthenticationState(ClaimsPrincipal user)
        {
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
            return Task.CompletedTask;
        }
    }
}
