using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.QnAMessages;
using System.Security.Claims;

namespace GHSMSystem.Controllers
{
    [Route("api/QnAMessage")]
    [ApiController]
    public class QnAMessageController : ControllerBase
    {
        private readonly IQnAMessageService _qnAMessageService;

        public QnAMessageController(IQnAMessageService qnAMessageService)
        { 
            _qnAMessageService = qnAMessageService;
        }

        [HttpPost("{questionId}")]
        [Authorize]
        public async Task<IActionResult> CreateQnAMessage(int questionId, [FromBody] CreateQnAMessageRequest request)
        {
            var senderId = User.FindFirstValue("AccountID");
            var result = await _qnAMessageService.CreateQnAMessageAsync(senderId, questionId, request);
            if (result.StatusCode == StatusCodeEnum.Created_201)
                return CreatedAtAction(nameof(GetQnAMessagesByQuestionId), new { questionId = questionId }, result);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{questionId}")]
        [Authorize]
        public async Task<IActionResult> GetQnAMessagesByQuestionId(int questionId)
        {
            var result = await _qnAMessageService.GetQnAMessagesByQuestionAsync(questionId);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
