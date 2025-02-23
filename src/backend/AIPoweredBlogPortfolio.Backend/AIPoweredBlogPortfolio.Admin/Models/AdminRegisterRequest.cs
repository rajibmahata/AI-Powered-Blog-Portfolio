using System.ComponentModel.DataAnnotations;

public class AdminRegisterRequest
{
    public int AdminId { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [StringLength(100, ErrorMessage = "Username must be between 3 and 100 characters", MinimumLength = 3)]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, ErrorMessage = "Password must be between 5 and 100 characters", MinimumLength = 5)]
    public string Password { get; set; }
}
