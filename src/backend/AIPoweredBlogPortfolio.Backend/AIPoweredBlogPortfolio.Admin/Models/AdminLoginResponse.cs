using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.Admin.Models
{
    public class AdminLoginResponse: BaseResponse
    {
        public string AdminId { get; set; }
        public string Token { get; set; }
    }
}
 
