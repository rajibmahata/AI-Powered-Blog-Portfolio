using AIPoweredBlogPortfolio.Admin.Models;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace AIPoweredBlogPortfolio.Admin.Services
{
    public class BlogPostClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BlogPostClient> _logger;
        private readonly ILocalStorageService _localStorage;

        public BlogPostClient(HttpClient httpClient, ILogger<BlogPostClient> logger, ILocalStorageService localStorage)
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

        public async Task<IEnumerable<BlogPostResponse>> GetBlogPostsAsync(string token)
        {
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.GetAsync("api/BlogPosts");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<BlogPostResponse>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting blog posts.");
                throw;
            }
        }

        public async Task<BlogPostResponse> GetBlogPostAsync(int id, string token)
        {
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.GetAsync($"api/BlogPosts/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BlogPostResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting blog post by ID: {id}.");
                throw;
            }
        }

        public async Task<BlogPostResponse> CreateBlogPostAsync(BlogPostRequest blogPostRequest, string token)
        {
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.PostAsJsonAsync("api/BlogPosts", blogPostRequest);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BlogPostResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating blog post.");
                throw;
            }
        }

        public async Task UpdateBlogPostAsync(int id, BlogPostRequest blogPostRequest, string token)
        {
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.PutAsJsonAsync($"api/BlogPosts/{id}", blogPostRequest);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating blog post with ID: {id}.");
                throw;
            }
        }

        public async Task DeleteBlogPostAsync(int id, string token)
        {
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.DeleteAsync($"api/BlogPosts/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting blog post with ID: {id}.");
                throw;
            }
        }
    }
}
