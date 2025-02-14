using AIPoweredBlogPortfolio.API.dbContext;
using AIPoweredBlogPortfolio.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AIPoweredBlogPortfolio.API.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly ApplicationDbContext _context;

        public BlogPostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetBlogPostByIdAsync(int id)
        {
            return await _context.BlogPosts.FindAsync(id);
        }

        public async Task<BlogPost> CreateBlogPostAsync(BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task UpdateBlogPostAsync(BlogPost blogPost)
        {
            _context.Entry(blogPost).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBlogPostAsync(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost != null)
            {
                _context.BlogPosts.Remove(blogPost);
                await _context.SaveChangesAsync();
            }
        }
    }
}
