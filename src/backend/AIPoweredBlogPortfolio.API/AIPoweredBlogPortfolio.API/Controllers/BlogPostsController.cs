using AIPoweredBlogPortfolio.API.Models;
using AIPoweredBlogPortfolio.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace AIPoweredBlogPortfolio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostsController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all blog posts")]
        public async Task<ActionResult<IEnumerable<BlogPostResponse>>> GetBlogPosts()
        {
            var blogPosts = await _blogPostService.GetAllBlogPostsAsync();
            var response = blogPosts.Select(bp => new BlogPostResponse
            {
                PostId = bp.PostId,
                Title = bp.Title,
                ContentHtml = bp.ContentHtml,
                RawContent = bp.RawContent,
                Tags = bp.Tags,
                MetaDescription = bp.MetaDescription,
                CreatedAt = bp.CreatedAt,
                UpdatedAt = bp.UpdatedAt,
                AdminId = bp.AdminId
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a blog post by ID")]
        public async Task<ActionResult<BlogPostResponse>> GetBlogPost(int id)
        {
            var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            var response = new BlogPostResponse
            {
                PostId = blogPost.PostId,
                Title = blogPost.Title,
                ContentHtml = blogPost.ContentHtml,
                RawContent = blogPost.RawContent,
                Tags = blogPost.Tags,
                MetaDescription = blogPost.MetaDescription,
                CreatedAt = blogPost.CreatedAt,
                UpdatedAt = blogPost.UpdatedAt,
                AdminId = blogPost.AdminId
            };
            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new blog post")]
        [SwaggerRequestExample(typeof(BlogPostRequest), typeof(BlogPostRequestExample))]
        public async Task<ActionResult<BlogPostResponse>> PostBlogPost(BlogPostRequest blogPostRequest)
        {
            var blogPost = new BlogPost
            {
                Title = blogPostRequest.Title,
                ContentHtml = blogPostRequest.ContentHtml,
                RawContent = blogPostRequest.RawContent,
                Tags = blogPostRequest.Tags,
                MetaDescription = blogPostRequest.MetaDescription,
                AdminId = blogPostRequest.AdminId
            };
            await _blogPostService.CreateBlogPostAsync(blogPost);
            var response = new BlogPostResponse
            {
                PostId = blogPost.PostId,
                Title = blogPost.Title,
                ContentHtml = blogPost.ContentHtml,
                RawContent = blogPost.RawContent,
                Tags = blogPost.Tags,
                MetaDescription = blogPost.MetaDescription,
                CreatedAt = blogPost.CreatedAt,
                UpdatedAt = blogPost.UpdatedAt,
                AdminId = blogPost.AdminId
            };
            return CreatedAtAction("GetBlogPost", new { id = blogPost.PostId }, response);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing blog post")]
        [SwaggerRequestExample(typeof(BlogPostRequest), typeof(BlogPostRequestExample))]
        public async Task<IActionResult> PutBlogPost(int id, BlogPostRequest blogPostRequest)
        {
            var blogPost = new BlogPost
            {
                PostId = id,
                Title = blogPostRequest.Title,
                ContentHtml = blogPostRequest.ContentHtml,
                RawContent = blogPostRequest.RawContent,
                Tags = blogPostRequest.Tags,
                MetaDescription = blogPostRequest.MetaDescription,
                AdminId = blogPostRequest.AdminId
            };
            await _blogPostService.UpdateBlogPostAsync(blogPost);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a blog post")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            await _blogPostService.DeleteBlogPostAsync(id);
            return NoContent();
        }
    }

    public class BlogPostRequestExample : IExamplesProvider<BlogPostRequest>
    {
        public BlogPostRequest GetExamples()
        {
            return new BlogPostRequest
            {
                Title = "Sample Blog Post",
                ContentHtml = "<p>This is a sample blog post.</p>",
                RawContent = "This is a sample blog post.",
                Tags = "sample, blog",
                MetaDescription = "This is a sample blog post.",
                AdminId = 1
            };
        }
    }
}
