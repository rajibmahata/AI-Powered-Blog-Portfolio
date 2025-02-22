namespace AIPoweredBlogPortfolio.Admin.Services
{
    public interface IAiContentService
    {
        Task<string> GenerateTitle(string content);
        Task<string> RefineContent(string content);
        Task<List<string>> GenerateSeoKeywords(string content);
    }
}
