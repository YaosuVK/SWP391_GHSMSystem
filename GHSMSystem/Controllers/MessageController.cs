using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Message;
using System.Security.Claims;

namespace GHSMSystem.Controllers
{
    [Route("api/Message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }


        [HttpPost]
        [Authorize(Roles = "Customer,Consultant")]
        public async Task<IActionResult> CreateMessage(int questionId, [FromBody] CreateMessageRequest request)
        {
            var userId = User.FindFirstValue("AccountID");
            var result = await _messageService.CreateMessageAsync(userId, questionId, request);
            if (result.StatusCode == StatusCodeEnum.Created_201)
                return CreatedAtAction(nameof(GetMessagesByQuestion), new { questionId }, result);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesByQuestion(int questionId)
        {
            var result = await _messageService.GetMessagesByQuestionAsync(questionId);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
