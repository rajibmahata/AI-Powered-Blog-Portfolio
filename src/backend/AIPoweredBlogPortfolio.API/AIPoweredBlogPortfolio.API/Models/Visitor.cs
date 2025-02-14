using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.API.Models
{
    public class Visitor
    {
        [Key]
        public int VisitorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
