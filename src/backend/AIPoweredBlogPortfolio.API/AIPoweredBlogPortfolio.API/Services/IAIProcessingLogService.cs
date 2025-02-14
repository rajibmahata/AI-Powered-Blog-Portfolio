using AIPoweredBlogPortfolio.API.Models;

namespace AIPoweredBlogPortfolio.API.Services
{
    public interface IAIProcessingLogService
    {
        Task<IEnumerable<AIProcessingLog>> GetAllLogsAsync();
        Task<AIProcessingLog> GetLogByIdAsync(int id);
        Task<AIProcessingLog> CreateLogAsync(AIProcessingLog log);
        Task UpdateLogAsync(AIProcessingLog log);
        Task DeleteLogAsync(int id);
    }
}

