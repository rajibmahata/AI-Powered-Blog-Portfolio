using AIPoweredBlogPortfolio.API.Models;

namespace AIPoweredBlogPortfolio.API.Services
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();
        Task<BlogPost> GetBlogPostByIdAsync(int id);
        Task<BlogPost> CreateBlogPostAsync(BlogPost blogPost);
        Task UpdateBlogPostAsync(BlogPost blogPost);
        Task DeleteBlogPostAsync(int id);

    }
}
