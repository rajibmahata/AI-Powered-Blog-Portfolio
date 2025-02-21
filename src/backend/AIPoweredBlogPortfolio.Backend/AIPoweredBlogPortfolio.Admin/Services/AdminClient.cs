using AIPoweredBlogPortfolio.Admin.Models;

namespace AIPoweredBlogPortfolio.Admin.Services
{
    public class AdminClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AdminClient> _logger;

        public AdminClient(HttpClient httpClient, ILogger<AdminClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> LoginAsync(AdminLoginRequest loginRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/admin/login", loginRequest);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<LoginResult>();
                return result.Token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging in.");
                throw;
            }
        }

        public async Task<IEnumerable<Admin>> GetAllAdminsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/admin");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<Admin>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all admins.");
                throw;
            }
        }

        public async Task<Admin> GetAdminByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/admin/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Admin>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting admin by ID: {id}.");
                throw;
            }
        }

        public async Task<Admin> CreateAdminAsync(AdminRegisterRequest registerRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/admin", registerRequest);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Admin>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating admin.");
                throw;
            }
        }

        public async Task<Admin> UpdateAdminAsync(int id, AdminUpdateRequest updateRequest)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/admin/{id}", updateRequest);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Admin>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating admin with ID: {id}.");
                throw;
            }
        }

        public async Task DeleteAdminAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/admin/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting admin with ID: {id}.");
                throw;
            }
        }

        private class LoginResult
        {
            public string Token { get; set; }
        }
    }
}
