namespace AIPoweredBlogPortfolio.API.Models
{
    public class AIProcessingLogResponse
    {
        public int LogId { get; set; }
        public int PostId { get; set; }
        public string ProcessingType { get; set; }
        public string ProcessingResult { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
