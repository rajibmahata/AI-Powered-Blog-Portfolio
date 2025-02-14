using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.API.Models
{
    public class AIProcessingLog
    {
        [Key]
        public int LogId { get; set; }
        [ForeignKey("BlogPost")]
        public int PostId { get; set; }
        public BlogPost BlogPost { get; set; }
        public string ProcessingType { get; set; }
        public string ProcessingResult { get; set; }
        public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    }
}
