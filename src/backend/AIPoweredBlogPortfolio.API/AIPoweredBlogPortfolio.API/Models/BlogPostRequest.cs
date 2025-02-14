using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.API.Models
{
    public class BlogPostRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ContentHtml { get; set; }
        [Required]
        public string RawContent { get; set; }
        public string Tags { get; set; }
        public string MetaDescription { get; set; }
        [Required]
        public int AdminId { get; set; }
    }
}
