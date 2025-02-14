using AIPoweredBlogPortfolio.API.Models;

namespace AIPoweredBlogPortfolio.API.Services
{
    public interface IAdminService
    {
        Task<Admin> Authenticate(string username, string password);
        Task<IEnumerable<Admin>> GetAll();
        Task<Admin> GetById(int id);
        Task<Admin> Create(Admin admin, string password);
        Task Update(Admin admin, string password = null);
        Task Delete(int id);
    }
}
