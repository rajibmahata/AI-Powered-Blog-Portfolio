using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.API.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
