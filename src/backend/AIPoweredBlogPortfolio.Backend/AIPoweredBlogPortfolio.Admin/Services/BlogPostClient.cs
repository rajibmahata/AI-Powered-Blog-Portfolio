using Blazored.LocalStorage;
using System.Net.Http.Headers;
using AIPoweredBlogPortfolio.Admin.Models;


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
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<BlogPostResponse>>();
                    return result;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Failed to retrieve blog posts. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, result: {result}");
                   return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting blog posts. Message: {ex.Message}, InnerException: {ex.InnerException}");
                throw ex;
            }
        }

        public async Task<BlogPostResponse> GetBlogPostAsync(int id, string token)
        {
            BlogPostResponse blogPostResponse = new BlogPostResponse
            {
                isSuccess = false
            };
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.GetAsync($"api/BlogPosts/{id}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Successfully retrieved blog post with ID: {id}.");
                    blogPostResponse = await response.Content.ReadFromJsonAsync<BlogPostResponse>();
                    blogPostResponse.isSuccess = true;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Failed to retrieve blog post with ID: {id}. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, result: {result}");
                    blogPostResponse.Message = $"Failed to retrieve blog post with ID: {id}. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, result: {result}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting blog post by ID: {id}.");
                blogPostResponse.Message = ex.Message;
            }

            return blogPostResponse;
        }

        public async Task<BlogPostResponse> CreateBlogPostAsync(BlogPostRequest blogPostRequest, string token)
        {
            BlogPostResponse blogPostResponse = new BlogPostResponse
            {
                isSuccess = false
            };
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.PostAsJsonAsync("api/BlogPosts", blogPostRequest);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully created a blog post.");
                    blogPostResponse = await response.Content.ReadFromJsonAsync<BlogPostResponse>();
                    blogPostResponse.isSuccess = true;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Failed to create blog post. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, result: {result}");
                    blogPostResponse.Message = $"Failed to create blog post. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, result: {result}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating blog post.");
                blogPostResponse.Message = ex.Message;
            }

            return blogPostResponse;
        }

        public async Task<BlogPostResponse> UpdateBlogPostAsync(int id, BlogPostRequest blogPostRequest, string token)
        {
            BlogPostResponse blogPostResponse = new BlogPostResponse
            {
                isSuccess = false
            };
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.PutAsJsonAsync($"api/BlogPosts/{id}", blogPostRequest);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Successfully updated blog post with ID: {id}.");
                    blogPostResponse.isSuccess = true;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Failed to update blog post with ID: {id}. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, result: {result}");
                    blogPostResponse.Message = $"Failed to update blog post with ID: {id}. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, result: {result}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating blog post with ID: {id}.");
                blogPostResponse.Message = ex.Message;
            }

            return blogPostResponse;
        }

        public async Task<BlogPostResponse> DeleteBlogPostAsync(int id, string token)
        {
            BlogPostResponse blogPostResponse = new BlogPostResponse
            {
                isSuccess = false
            };
            try
            {
                await AddJwtTokenAsync(token);
                var response = await _httpClient.DeleteAsync($"api/BlogPosts/{id}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Successfully deleted blog post with ID: {id}.");
                    blogPostResponse.isSuccess = true;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Failed to delete blog post with ID: {id}. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, result: {result}");
                    blogPostResponse.Message = $"Failed to delete blog post with ID: {id}. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, result: {result}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting blog post with ID: {id}.");
                blogPostResponse.Message = ex.Message;
            }

            return blogPostResponse;
        }
    }
}
