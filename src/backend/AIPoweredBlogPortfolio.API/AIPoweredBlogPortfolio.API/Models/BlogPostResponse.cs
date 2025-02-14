namespace AIPoweredBlogPortfolio.API.Models
{
    public class BlogPostResponse
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string ContentHtml { get; set; }
        public string RawContent { get; set; }
        public string Tags { get; set; }
        public string MetaDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AdminId { get; set; }
    }
}
