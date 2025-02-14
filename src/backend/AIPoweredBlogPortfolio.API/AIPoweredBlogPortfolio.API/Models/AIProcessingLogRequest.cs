using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.API.Models
{
    public class AIProcessingLogRequest
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string ProcessingType { get; set; }
        [Required]
        public string ProcessingResult { get; set; }
    }
}
