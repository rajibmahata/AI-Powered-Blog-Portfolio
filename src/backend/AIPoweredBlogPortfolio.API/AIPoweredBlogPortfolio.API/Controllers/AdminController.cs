using AIPoweredBlogPortfolio.API.Models;
using AIPoweredBlogPortfolio.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
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
        [SwaggerOperation(Summary = "Authenticates an admin and generates a JWT token")]
        [SwaggerRequestExample(typeof(AdminLoginModel), typeof(AdminLoginModelExample))]
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
        [SwaggerOperation(Summary = "Gets all admins")]
        public async Task<IActionResult> GetAll()
        {
            var admins = await _adminService.GetAll();
            return Ok(admins);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets an admin by ID")]
        public async Task<IActionResult> GetById(int id)
        {
            var admin = await _adminService.GetById(id);
            return Ok(admin);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new admin")]
        [SwaggerRequestExample(typeof(AdminRegisterModel), typeof(AdminRegisterModelExample))]
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
        [SwaggerOperation(Summary = "Updates an existing admin")]
        [SwaggerRequestExample(typeof(AdminUpdateModel), typeof(AdminUpdateModelExample))]
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
        [SwaggerOperation(Summary = "Deletes an admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.Delete(id);
            return Ok(new { message = "Admin deleted successfully" });
        }

        private string GenerateJwtToken(Admin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configValue.JWTSecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, admin.Username),
            new Claim(ClaimTypes.Role, "admin")
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                Audience = _configValue.audience,
                Issuer = _configValue.issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }

    // ... existing code ...

    public class AdminLoginModelExample : IExamplesProvider<AdminLoginModel>
    {
        public AdminLoginModel GetExamples()
        {
            return new AdminLoginModel
            {
                Username = "admin1",
                Password = "admin"
            };
        }
    }

    public class AdminRegisterModelExample : IExamplesProvider<AdminRegisterModel>
    {
        public AdminRegisterModel GetExamples()
        {
            return new AdminRegisterModel
            {
                Username = "newadmin",
                Email = "newadmin@example.com",
                Password = "password123"
            };
        }
    }

    public class AdminUpdateModelExample : IExamplesProvider<AdminUpdateModel>
    {
        public AdminUpdateModel GetExamples()
        {
            return new AdminUpdateModel
            {
                Username = "updatedadmin",
                Email = "updatedadmin@example.com",
                Password = "newpassword123"
            };
        }
    }
}


