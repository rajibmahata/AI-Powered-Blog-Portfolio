using AIPoweredBlogPortfolio.API.dbContext;
using AIPoweredBlogPortfolio.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.OpenApi.Models;
using AIPoweredBlogPortfolio.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using AIPoweredBlogPortfolio.API.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


// Add builder.Services to the container.
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 443;
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<ConfigValue>(builder.Configuration.GetSection("Settings"));

builder.Services.AddLogging(configure => configure.AddConsole());

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), new MySqlServerVersion(new Version(8, 0, 36))));
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<IAIProcessingLogService, AIProcessingLogService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var config = builder.Configuration.GetSection("Settings").Get<ConfigValue>();
        options.Authority = config.Authority;
        var IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JWTSecretKey));
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config.issuer,
            ValidAudience = config.audience,
            IssuerSigningKey = IssuerSigningKey
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "admin"));
});


// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AI-Powered Blog Portfolio API", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    c.ExampleFilters(); // Ensure this line is present
});

// Register the example providers
builder.Services.AddSwaggerExamplesFromAssemblyOf<AdminLoginModelExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<AdminRegisterModelExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<AdminUpdateModelExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<BlogPostRequestExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<VisitorRequestExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<AIProcessingLogRequestExample>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AI-Powered Blog Portfolio API V1");
        c.RoutePrefix = string.Empty;  // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
