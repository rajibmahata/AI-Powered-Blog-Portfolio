using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.Admin.Models
{
    public class AdminLoginRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
 
