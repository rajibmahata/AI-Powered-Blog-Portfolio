using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.API.Models
{
    public class BlogPostRequest
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string ContentHtml { get; set; }
        [Required]
        public required string RawContent { get; set; }
        public string Tags { get; set; }
        public string MetaDescription { get; set; }
        [Required]
        public int AdminId { get; set; }
    }
}
