using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace AIPoweredBlogPortfolio.API.Models
{
    public class VisitorRequest
    {
        [Required]
        [MaxLength(45)]
        public string IPAddress { get; set; }

        public int? UserID { get; set; }  // NULL for anonymous users

        [Required]
        public string UserAgent { get; set; }

        [Required]
        [MaxLength(255)]
        public string PageVisited { get; set; }

        [Required]
        public DateTime VisitTimestamp { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        public DeviceType DeviceType { get; set; }

        [MaxLength(255)]
        public string Browser { get; set; }

        [MaxLength(255)]
        public string SessionID { get; set; }

        [MaxLength(255)]
        public string ReferrerURL { get; set; }

        public string InterestsJson { get; set; }  // Stores AI-analyzed interests as key-value pairs

        public int TimeSpent { get; set; }  // In seconds
       
    }
}
