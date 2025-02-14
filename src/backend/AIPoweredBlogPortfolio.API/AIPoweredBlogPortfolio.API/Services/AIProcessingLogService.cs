using AIPoweredBlogPortfolio.API.dbContext;
using AIPoweredBlogPortfolio.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AIPoweredBlogPortfolio.API.Services
{
    public class AIProcessingLogService : IAIProcessingLogService
    {
        private readonly ApplicationDbContext _context;

        public AIProcessingLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AIProcessingLog>> GetAllLogsAsync()
        {
            return await _context.AIProcessingLogs.ToListAsync();
        }

        public async Task<AIProcessingLog> GetLogByIdAsync(int id)
        {
            return await _context.AIProcessingLogs.FindAsync(id);
        }

        public async Task<AIProcessingLog> CreateLogAsync(AIProcessingLog log)
        {
            _context.AIProcessingLogs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task UpdateLogAsync(AIProcessingLog log)
        {
            _context.Entry(log).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogAsync(int id)
        {
            var log = await _context.AIProcessingLogs.FindAsync(id);
            if (log != null)
            {
                _context.AIProcessingLogs.Remove(log);
                await _context.SaveChangesAsync();
            }
        }
    }
}

