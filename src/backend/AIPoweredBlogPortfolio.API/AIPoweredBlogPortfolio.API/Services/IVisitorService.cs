using AIPoweredBlogPortfolio.API.Models;

namespace AIPoweredBlogPortfolio.API.Services
{
    public interface IVisitorService
    {
        Task<IEnumerable<Visitor>> GetAllVisitorsAsync();
        Task<Visitor> GetVisitorByIdAsync(int id);
        Task<Visitor> CreateVisitorAsync(Visitor visitor);
        Task UpdateVisitorAsync(Visitor visitor);
        Task DeleteVisitorAsync(int id);
    }
}

