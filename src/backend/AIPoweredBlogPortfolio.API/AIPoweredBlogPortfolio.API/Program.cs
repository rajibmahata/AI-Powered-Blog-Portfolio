using AIPoweredBlogPortfolio.API.dbContext;
using AIPoweredBlogPortfolio.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.OpenApi.Models;
using AIPoweredBlogPortfolio.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<ConfigValue>(builder.Configuration.GetSection("Settings"));

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), new MySqlServerVersion(new Version(8, 0, 36))));
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<IAIProcessingLogService, AIProcessingLogService>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var config = builder.Configuration.GetSection("Settings").Get<ConfigValue>();
        options.Authority = config.Authority;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseRouting();


app.Run();
