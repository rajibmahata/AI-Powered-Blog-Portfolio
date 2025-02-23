using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AIPoweredBlogPortfolio.Admin.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState _anonymousState = new(new ClaimsPrincipal(new ClaimsIdentity()));
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<CustomAuthenticationStateProvider> _logger;
        private bool _jsInteropReady = false;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage, ILogger<CustomAuthenticationStateProvider> logger)
        {
            _localStorage = localStorage;
            _logger = logger;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!_jsInteropReady)
            {
                return _anonymousState; // JS Interop is not available yet.
            }

            try
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");

                if (string.IsNullOrWhiteSpace(token))
                {
                    return _anonymousState; // No token → Not authenticated
                }

                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                if (jwtToken.ValidTo < DateTime.UtcNow.AddMinutes(-10))
                {
                    await MarkUserAsLoggedOut(); // Token expired → Log out
                    return _anonymousState;
                }

                var user = GetUserFromJwtToken(token);
                return new AuthenticationState(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving authentication state.");
                return _anonymousState;
            }
        }

        public async Task InitializeAsync()
        {
            _jsInteropReady = true; // Now JS interop is available
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private ClaimsPrincipal GetUserFromJwtToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");

                if (!identity.HasClaim(c => c.Type == ClaimTypes.Name))
                {
                    var username = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value
                                   ?? jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                    if (!string.IsNullOrEmpty(username))
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Name, username));
                    }
                }
                return new ClaimsPrincipal(identity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid JWT Token.");
                return new ClaimsPrincipal(new ClaimsIdentity()); // Invalid token → Not authenticated
            }
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            try
            {
                if (!_jsInteropReady)
                {
                    await InitializeAsync(); // Ensure LocalStorage is available
                }

                await _localStorage.SetItemAsync("authToken", token);

                if (!_jsInteropReady)
                    return;

                var user = GetUserFromJwtToken(token);
                var authState = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(authState));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking user as authenticated.");
            }
        }

        public async Task MarkUserAsLoggedOut()
        {
            try
            {
                await _localStorage.RemoveItemAsync("authToken");

                if (!_jsInteropReady)
                    return;

                NotifyAuthenticationStateChanged(Task.FromResult(_anonymousState));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking user as logged out.");
            }
        }
    }
}
