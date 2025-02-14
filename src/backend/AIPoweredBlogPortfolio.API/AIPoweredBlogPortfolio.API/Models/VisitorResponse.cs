namespace AIPoweredBlogPortfolio.API.Models
{
    public class VisitorResponse
    {
        public int VisitorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
