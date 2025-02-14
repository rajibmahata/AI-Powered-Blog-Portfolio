using AIPoweredBlogPortfolio.API.Models;
using AIPoweredBlogPortfolio.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AIPoweredBlogPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ConfigValue _configValue;
        public AdminController(IAdminService adminService, IOptions<ConfigValue> configValue)
        {
            _adminService = adminService;
            _configValue = configValue.Value;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] AdminLoginModel model)
        {
            var admin = await _adminService.Authenticate(model.Username, model.Password);

            if (admin == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var token = GenerateJwtToken(admin);
            return Ok(new { token });
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var admins = await _adminService.GetAll();
            return Ok(admins);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var admin = await _adminService.GetById(id);
            return Ok(admin);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdminRegisterModel model)
        {
            var admin = new Admin
            {
                Username = model.Username,
                Email = model.Email
            };

            try
            {
                await _adminService.Create(admin, model.Password);
                return Ok(admin);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AdminUpdateModel model)
        {
            var admin = await _adminService.GetById(id);
            if (admin == null)
                return NotFound();

            admin.Username = model.Username;
            admin.Email = model.Email;

            try
            {
                await _adminService.Update(admin, model.Password);
                return Ok(admin);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.Delete(id);
            return Ok(new { message = "Admin deleted successfully" });
        }

        private string GenerateJwtToken(Admin admin)
        {
            // Implement JWT token generation logic here.
            // This is a placeholder and should be replaced with actual implementation.
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configValue.JWTSecretKey); // Replace with your actual secret key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, admin.Username),
                    new Claim(ClaimTypes.Role, "admin")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
