using System.Text.Json;

namespace AIPoweredBlogPortfolio.API.Models
{
    public class VisitorResponse
    {
        public int VisitorID { get; set; }
        public string IPAddress { get; set; }
        public int? UserID { get; set; }  // NULL for anonymous users
        public string UserAgent { get; set; }
        public string PageVisited { get; set; }
        public DateTime VisitTimestamp { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DeviceType DeviceType { get; set; }
        public string Browser { get; set; }
        public string SessionID { get; set; }
        public string ReferrerURL { get; set; }
        public string InterestsJson { get; set; }  // Stores AI-analyzed interests as key-value pairs
        public int TimeSpent { get; set; }  // In seconds
    }
}
