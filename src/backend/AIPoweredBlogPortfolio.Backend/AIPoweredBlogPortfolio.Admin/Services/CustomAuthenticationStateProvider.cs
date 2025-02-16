using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AIPoweredBlogPortfolio.Admin.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(anonymous);
        }

        public Task MarkUserAsAuthenticated(string username)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
            return Task.CompletedTask;
        }

        public Task MarkUserAsLoggedOut()
        {
            var authState = Task.FromResult(anonymous);
            NotifyAuthenticationStateChanged(authState);
            return Task.CompletedTask;
        }
    }
}
