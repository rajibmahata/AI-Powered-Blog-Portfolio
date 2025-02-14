using AIPoweredBlogPortfolio.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AIPoweredBlogPortfolio.API.dbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<AIProcessingLog> AIProcessingLogs { get; set; }
    }
}
