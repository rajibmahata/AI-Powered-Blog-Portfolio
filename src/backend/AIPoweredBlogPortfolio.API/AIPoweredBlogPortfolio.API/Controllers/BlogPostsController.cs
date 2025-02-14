using AIPoweredBlogPortfolio.API.Models;
using AIPoweredBlogPortfolio.API.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
        {
            return Ok(await _blogPostService.GetAllBlogPostsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return Ok(blogPost);
        }

        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPost blogPost)
        {
            await _blogPostService.CreateBlogPostAsync(blogPost);
            return CreatedAtAction("GetBlogPost", new { id = blogPost.PostId }, blogPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(int id, BlogPost blogPost)
        {
            if (id != blogPost.PostId)
            {
                return BadRequest();
            }

            await _blogPostService.UpdateBlogPostAsync(blogPost);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            await _blogPostService.DeleteBlogPostAsync(id);
            return NoContent();
        }
    }
}
