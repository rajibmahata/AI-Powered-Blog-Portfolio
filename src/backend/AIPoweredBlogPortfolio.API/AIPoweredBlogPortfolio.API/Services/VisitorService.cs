using AIPoweredBlogPortfolio.API.dbContext;
using AIPoweredBlogPortfolio.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AIPoweredBlogPortfolio.API.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly ApplicationDbContext _context;

        public VisitorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitorsAsync()
        {
            return await _context.Visitors.ToListAsync();
        }

        public async Task<Visitor> GetVisitorByIdAsync(int id)
        {
            return await _context.Visitors.FindAsync(id);
        }

        public async Task<Visitor> CreateVisitorAsync(Visitor visitor)
        {
            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();
            return visitor;
        }

        public async Task UpdateVisitorAsync(Visitor visitor)
        {
            _context.Entry(visitor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVisitorAsync(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor != null)
            {
                _context.Visitors.Remove(visitor);
                await _context.SaveChangesAsync();
            }
        }
    }
}

