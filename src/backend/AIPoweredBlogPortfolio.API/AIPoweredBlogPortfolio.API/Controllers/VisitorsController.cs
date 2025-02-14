using AIPoweredBlogPortfolio.API.Models;
using AIPoweredBlogPortfolio.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace AIPoweredBlogPortfolio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        private readonly IVisitorService _visitorService;

        public VisitorsController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all visitors")]
        public async Task<ActionResult<IEnumerable<VisitorResponse>>> GetVisitors()
        {
            var visitors = await _visitorService.GetAllVisitorsAsync();
            var response = visitors.Select(v => new VisitorResponse
            {
                VisitorId = v.VisitorId,
                Name = v.Name,
                Email = v.Email,
                Message = v.Message,
                SubmittedAt = v.SubmittedAt
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets a visitor by ID")]
        public async Task<ActionResult<VisitorResponse>> GetVisitor(int id)
        {
            var visitor = await _visitorService.GetVisitorByIdAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }
            var response = new VisitorResponse
            {
                VisitorId = visitor.VisitorId,
                Name = visitor.Name,
                Email = visitor.Email,
                Message = visitor.Message,
                SubmittedAt = visitor.SubmittedAt
            };
            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new visitor")]
        [SwaggerRequestExample(typeof(VisitorRequest), typeof(VisitorRequestExample))]
        public async Task<ActionResult<VisitorResponse>> PostVisitor(VisitorRequest visitorRequest)
        {
            var visitor = new Visitor
            {
                Name = visitorRequest.Name,
                Email = visitorRequest.Email,
                Message = visitorRequest.Message
            };
            await _visitorService.CreateVisitorAsync(visitor);
            var response = new VisitorResponse
            {
                VisitorId = visitor.VisitorId,
                Name = visitor.Name,
                Email = visitor.Email,
                Message = visitor.Message,
                SubmittedAt = visitor.SubmittedAt
            };
            return CreatedAtAction("GetVisitor", new { id = visitor.VisitorId }, response);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing visitor")]
        [SwaggerRequestExample(typeof(VisitorRequest), typeof(VisitorRequestExample))]
        public async Task<IActionResult> PutVisitor(int id, VisitorRequest visitorRequest)
        {
            var visitor = new Visitor
            {
                VisitorId = id,
                Name = visitorRequest.Name,
                Email = visitorRequest.Email,
                Message = visitorRequest.Message
            };
            await _visitorService.UpdateVisitorAsync(visitor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a visitor")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            await _visitorService.DeleteVisitorAsync(id);
            return NoContent();
        }
    }

    public class VisitorRequestExample : IExamplesProvider<VisitorRequest>
    {
        public VisitorRequest GetExamples()
        {
            return new VisitorRequest
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Message = "This is a sample message."
            };
        }
    }
}
