using AIPoweredBlogPortfolio.Admin.Models;
using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AIPoweredBlogPortfolio.Admin.Services
{
    public class AdminClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AdminClient> _logger;
        private readonly ILocalStorageService _localStorage;

        public AdminClient(HttpClient httpClient, ILogger<AdminClient> logger, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _logger = logger;
            _localStorage = localStorage;
        }

        private async Task AddJwtTokenAsync(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<AdminLoginResponse> LoginAsync(AdminLoginRequest loginRequest)
        {
            AdminLoginResponse adminLoginResponse = new AdminLoginResponse();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/admin/login", loginRequest);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<AdminLoginResponse>();
                    if (loginResponse != null)
                    {
                        adminLoginResponse = loginResponse;
                        adminLoginResponse.isSuccess = true;
                    }
                    else
                    {
                        adminLoginResponse.isSuccess = false;
                    }
                }
                else
                {
                    adminLoginResponse.isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                adminLoginResponse.isSuccess = false;
                _logger.LogError(ex, "Error occurred while logging in.");
            }
            return adminLoginResponse;
        }

        public async Task<IEnumerable<AdminViewModel>> GetAllAdminsAsync(string token)
        {
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.GetAsync("api/admin");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<AdminViewModel>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all admins.");
                throw;
            }
        }

        public async Task<AdminRegisterResponse> GetAdminByIdAsync(int id, string token)
        {
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.GetAsync($"api/admin/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AdminRegisterResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting admin by ID: {id}.");
                throw;
            }
        }

        public async Task<AdminRegisterResponse> CreateAdminAsync(AdminRegisterRequest registerRequest, string token)
        {
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.PostAsJsonAsync("api/admin", registerRequest);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AdminRegisterResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating admin.");
                throw;
            }
        }

        public async Task<AdminRegisterResponse> UpdateAdminAsync(int id, AdminUpdateRequest updateRequest, string token)
        {
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.PutAsJsonAsync($"api/admin/{id}", updateRequest);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AdminRegisterResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating admin with ID: {id}.");
                throw;
            }
        }

        public async Task DeleteAdminAsync(int id, string token)
        {
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.DeleteAsync($"api/admin/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting admin with ID: {id}.");
                throw;
            }
        }
    }
}
