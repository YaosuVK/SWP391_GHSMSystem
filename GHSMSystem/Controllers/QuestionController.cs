using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Question;
using System.Security.Claims;
using Service.RequestAndResponse.Request.QnAMessages;

namespace GHSMSystem.Controllers
{
    [Route("api/Question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IQnAMessageService _qnAMessageService;

        public QuestionController(IQuestionService questionService, IQnAMessageService qnAMessageService)
        {
            _questionService = questionService;
            _qnAMessageService = qnAMessageService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request)
        {
            var customerId = User.FindFirstValue("AccountID");
            var result = await _questionService.CreateQuestionAsync(customerId, request);
            if (result.StatusCode == StatusCodeEnum.Created_201)
                return CreatedAtAction(nameof(GetQuestionById), new { questionId = result.Data.QuestionID }, result);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var result = await _questionService.GetAllQuestionsAsync();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{questionId}")]
        public async Task<IActionResult> GetQuestionById(int questionId)
        {
            var result = await _questionService.GetQuestionByIdAsync(questionId);
            if (result.StatusCode == StatusCodeEnum.OK_200)
                return Ok(result);
            return NotFound(result);
        }

        [HttpPost("{questionId}/message")]
        [Authorize]
        public async Task<IActionResult> CreateQnAMessage(int questionId, [FromBody] CreateQnAMessageRequest request)
        {
            var senderId = User.FindFirstValue("AccountID");
            var result = await _qnAMessageService.CreateQnAMessageAsync(senderId, questionId, request);
            if (result.StatusCode == StatusCodeEnum.Created_201)
                return CreatedAtAction(nameof(GetQnAMessagesByQuestionId), new { questionId = questionId }, result);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{questionId}/messages")]
        [Authorize]
        public async Task<IActionResult> GetQnAMessagesByQuestionId(int questionId)
        {
            var result = await _qnAMessageService.GetQnAMessagesByQuestionAsync(questionId);
            return StatusCode((int)result.StatusCode, result);
        }

    }
}
