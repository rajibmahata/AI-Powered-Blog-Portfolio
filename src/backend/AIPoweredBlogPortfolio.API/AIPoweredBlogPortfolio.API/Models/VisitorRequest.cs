using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.API.Models
{
    public class VisitorRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
