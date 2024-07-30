using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.Server.Data;
using RealTimeChat.Server.Services;
using RealTimeChat.Shared;
using System.Security.Claims;

namespace RealTimeChat.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ChatDbContext context;
        private readonly ITextAnalyzeService textAnalyzeService;
        public MessagesController(ChatDbContext context, ITextAnalyzeService textAnalyzeService)
        {
            this.context = context;
            this.textAnalyzeService = textAnalyzeService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessageAsync(ChatMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.Message) || string.IsNullOrWhiteSpace(message.FromUserName))
            {
                return BadRequest("Invalid message");
            }
            message.CreatedDate = DateTime.Now;
            await context.ChatMessages.AddAsync(message);
            return Ok(await context.SaveChangesAsync());
        }

        [HttpPost("analize-sentiment")]
        public async Task<IActionResult> GetMessageAnalyizeAsync([FromBody] string message)
        {
            var sentimentResult = await textAnalyzeService.GetAnalyzeSentimentResult(message);
            if (sentimentResult != null)
            {
                return Ok(new SentimentResult() { Value = sentimentResult ?? Sentiment.Neutral });
            }

            return BadRequest("Invalid message result");

        }
    }
}
