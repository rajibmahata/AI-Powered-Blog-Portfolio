namespace AIPoweredBlogPortfolio.API.Models
{
    public class ConfigValue
    {
        public required string JWTSecretKey { get; set; }
        public required string Authority { get; set; }
        public required string issuer { get; set; }
        public required string audience { get; set; }
    }
}
