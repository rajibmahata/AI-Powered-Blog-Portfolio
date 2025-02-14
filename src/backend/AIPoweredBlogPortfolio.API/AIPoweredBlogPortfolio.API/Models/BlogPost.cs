using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.API.Models
{
    public class BlogPost
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ContentHtml { get; set; }
        [Required]
        public string RawContent { get; set; }
        public string Tags { get; set; }
        public string MetaDescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}
