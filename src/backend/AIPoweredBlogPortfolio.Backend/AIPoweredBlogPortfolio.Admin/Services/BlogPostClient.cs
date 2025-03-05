using AIPoweredBlogPortfolio.Admin.Models;
using AIPoweredBlogPortfolio.Admin.Services;
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
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully retrieved blog posts.");
                    return await response.Content.ReadFromJsonAsync<IEnumerable<BlogPostResponse>>();
                }
                else
                {
                    _logger.LogWarning($"Failed to retrieve blog posts. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    response.EnsureSuccessStatusCode();
                }
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
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Successfully retrieved blog post with ID: {id}.");
                    return await response.Content.ReadFromJsonAsync<BlogPostResponse>();
                }
                else
                {
                    _logger.LogWarning($"Failed to retrieve blog post with ID: {id}. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    response.EnsureSuccessStatusCode();
                }
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
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully created a blog post.");
                    return await response.Content.ReadFromJsonAsync<BlogPostResponse>();
                }
                else
                {
                    _logger.LogWarning($"Failed to create blog post. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    response.EnsureSuccessStatusCode();
                }
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
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Successfully updated blog post with ID: {id}.");
                }
                else
                {
                    _logger.LogWarning($"Failed to update blog post with ID: {id}. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    response.EnsureSuccessStatusCode();
                }
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
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Successfully deleted blog post with ID: {id}.");
                }
                else
                {
                    _logger.LogWarning($"Failed to delete blog post with ID: {id}. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting blog post with ID: {id}.");
                throw;
            }
        }
    }
}
```

This updated code logs status codes and includes the reason phrase for non-200 responses.