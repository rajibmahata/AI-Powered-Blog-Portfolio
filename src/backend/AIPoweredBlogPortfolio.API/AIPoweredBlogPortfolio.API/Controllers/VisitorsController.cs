using AIPoweredBlogPortfolio.API.Models;
using AIPoweredBlogPortfolio.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json;

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
                VisitorID = v.VisitorID,
                IPAddress = v.IPAddress,
                UserID = v.UserID,
                UserAgent = v.UserAgent,
                PageVisited = v.PageVisited,
                VisitTimestamp = v.VisitTimestamp,
                Country = v.Country,
                City = v.City,
                DeviceType = v.DeviceType,
                Browser = v.Browser,
                SessionID = v.SessionID,
                ReferrerURL = v.ReferrerURL,
                InterestsJson = v.InterestsJson,
                TimeSpent = v.TimeSpent
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
                VisitorID = visitor.VisitorID,
                IPAddress = visitor.IPAddress,
                UserID = visitor.UserID,
                UserAgent = visitor.UserAgent,
                PageVisited = visitor.PageVisited,
                VisitTimestamp = visitor.VisitTimestamp,
                Country = visitor.Country,
                City = visitor.City,
                DeviceType = visitor.DeviceType,
                Browser = visitor.Browser,
                SessionID = visitor.SessionID,
                ReferrerURL = visitor.ReferrerURL,
                InterestsJson = visitor.InterestsJson,
                TimeSpent = visitor.TimeSpent
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
                IPAddress = visitorRequest.IPAddress,
                UserID = visitorRequest.UserID,
                UserAgent = visitorRequest.UserAgent,
                PageVisited = visitorRequest.PageVisited,
                VisitTimestamp = visitorRequest.VisitTimestamp,
                Country = visitorRequest.Country,
                City = visitorRequest.City,
                DeviceType = visitorRequest.DeviceType,
                Browser = visitorRequest.Browser,
                SessionID = visitorRequest.SessionID,
                ReferrerURL = visitorRequest.ReferrerURL,
                InterestsJson = visitorRequest.InterestsJson,
                TimeSpent = visitorRequest.TimeSpent
            };
            await _visitorService.CreateVisitorAsync(visitor);
            var response = new VisitorResponse
            {
                VisitorID = visitor.VisitorID,
                IPAddress = visitor.IPAddress,
                UserID = visitor.UserID,
                UserAgent = visitor.UserAgent,
                PageVisited = visitor.PageVisited,
                VisitTimestamp = visitor.VisitTimestamp,
                Country = visitor.Country,
                City = visitor.City,
                DeviceType = visitor.DeviceType,
                Browser = visitor.Browser,
                SessionID = visitor.SessionID,
                ReferrerURL = visitor.ReferrerURL,
                InterestsJson = visitor.InterestsJson,
                TimeSpent = visitor.TimeSpent
            };
            return CreatedAtAction("GetVisitor", new { id = visitor.VisitorID }, response);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing visitor")]
        [SwaggerRequestExample(typeof(VisitorRequest), typeof(VisitorRequestExample))]
        public async Task<IActionResult> PutVisitor(int id, VisitorRequest visitorRequest)
        {
            var visitor = new Visitor
            {
                VisitorID = id,
                IPAddress = visitorRequest.IPAddress,
                UserID = visitorRequest.UserID,
                UserAgent = visitorRequest.UserAgent,
                PageVisited = visitorRequest.PageVisited,
                VisitTimestamp = visitorRequest.VisitTimestamp,
                Country = visitorRequest.Country,
                City = visitorRequest.City,
                DeviceType = visitorRequest.DeviceType,
                Browser = visitorRequest.Browser,
                SessionID = visitorRequest.SessionID,
                ReferrerURL = visitorRequest.ReferrerURL,
                InterestsJson = visitorRequest.InterestsJson,
                TimeSpent = visitorRequest.TimeSpent
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
                IPAddress = "192.168.1.1",
                UserID = 1,
                UserAgent = "Mozilla/5.0",
                PageVisited = "/home",
                VisitTimestamp = DateTime.UtcNow,
                Country = "USA",
                City = "New York",
                DeviceType = DeviceType.Desktop,
                Browser = "Chrome",
                SessionID = "session123",
                ReferrerURL = "http://example.com",
                InterestsJson = "{\"interest1\":\"value1\"}",
                TimeSpent = 120
            };
        }
    }
}

