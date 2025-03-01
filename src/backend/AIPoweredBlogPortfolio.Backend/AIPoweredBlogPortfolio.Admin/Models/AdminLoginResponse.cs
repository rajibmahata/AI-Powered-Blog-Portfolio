using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.Admin.Models
{
    public class AdminLoginResponse: BaseResponse
    {
        public int adminId { get; set; }
        public string token { get; set; }
    }
}
 
