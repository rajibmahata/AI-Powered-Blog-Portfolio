﻿using System.ComponentModel.DataAnnotations;

namespace AIPoweredBlogPortfolio.Admin.Models
{
    public class AdminViewModel
    {
        public int AdminId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
