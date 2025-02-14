using AIPoweredBlogPortfolio.API.Models;
using AIPoweredBlogPortfolio.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace AIPoweredBlogPortfolio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIProcessingLogsController : ControllerBase
    {
        private readonly IAIProcessingLogService _logService;

        public AIProcessingLogsController(IAIProcessingLogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all AI processing logs")]
        public async Task<ActionResult<IEnumerable<AIProcessingLogResponse>>> GetLogs()
        {
            var logs = await _logService.GetAllLogsAsync();
            var response = logs.Select(log => new AIProcessingLogResponse
            {
                LogId = log.LogId,
                PostId = log.PostId,
                ProcessingType = log.ProcessingType,
                ProcessingResult = log.ProcessingResult,
                ProcessedAt = log.ProcessedAt
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets an AI processing log by ID")]
        public async Task<ActionResult<AIProcessingLogResponse>> GetLog(int id)
        {
            var log = await _logService.GetLogByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            var response = new AIProcessingLogResponse
            {
                LogId = log.LogId,
                PostId = log.PostId,
                ProcessingType = log.ProcessingType,
                ProcessingResult = log.ProcessingResult,
                ProcessedAt = log.ProcessedAt
            };
            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new AI processing log")]
        [SwaggerRequestExample(typeof(AIProcessingLogRequest), typeof(AIProcessingLogRequestExample))]
        public async Task<ActionResult<AIProcessingLogResponse>> PostLog(AIProcessingLogRequest logRequest)
        {
            var log = new AIProcessingLog
            {
                PostId = logRequest.PostId,
                ProcessingType = logRequest.ProcessingType,
                ProcessingResult = logRequest.ProcessingResult
            };
            await _logService.CreateLogAsync(log);
            var response = new AIProcessingLogResponse
            {
                LogId = log.LogId,
                PostId = log.PostId,
                ProcessingType = log.ProcessingType,
                ProcessingResult = log.ProcessingResult,
                ProcessedAt = log.ProcessedAt
            };
            return CreatedAtAction("GetLog", new { id = log.LogId }, response);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates an existing AI processing log")]
        [SwaggerRequestExample(typeof(AIProcessingLogRequest), typeof(AIProcessingLogRequestExample))]
        public async Task<IActionResult> PutLog(int id, AIProcessingLogRequest logRequest)
        {
            var log = new AIProcessingLog
            {
                LogId = id,
                PostId = logRequest.PostId,
                ProcessingType = logRequest.ProcessingType,
                ProcessingResult = logRequest.ProcessingResult
            };
            await _logService.UpdateLogAsync(log);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes an AI processing log")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            await _logService.DeleteLogAsync(id);
            return NoContent();
        }
    }

    public class AIProcessingLogRequestExample : IExamplesProvider<AIProcessingLogRequest>
    {
        public AIProcessingLogRequest GetExamples()
        {
            return new AIProcessingLogRequest
            {
                PostId = 1,
                ProcessingType = "Sample Processing Type",
                ProcessingResult = "Sample Processing Result"
            };
        }
    }
}
