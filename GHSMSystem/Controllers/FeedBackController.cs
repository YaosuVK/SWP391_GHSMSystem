using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.FeedBacks;
using System.ComponentModel.DataAnnotations;

namespace GHSMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackService _feedBackService;

        public FeedBackController(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedBacks()
        {
            var result = await _feedBackService.GetAllFeedBacksAsync();
            if (result.StatusCode == StatusCodeEnum.OK_200)
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedBackById(int id)
        {
            var result = await _feedBackService.GetFeedBackByIdAsync(id);
            if (result.StatusCode == StatusCodeEnum.OK_200)
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetFeedBacksByCustomerId(string customerId)
        {
            var result = await _feedBackService.GetFeedBacksByCustomerIdAsync(customerId);
            if (result.StatusCode == StatusCodeEnum.OK_200)
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetFeedBacksByAppointmentId(int appointmentId)
        {
            var result = await _feedBackService.GetFeedBacksByAppointmentIdAsync(appointmentId);
            if (result.StatusCode == StatusCodeEnum.OK_200)
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedBack([FromBody] CreateFeedBackRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _feedBackService.CreateFeedBackAsync(request);
            if (result.StatusCode == StatusCodeEnum.OK_200)
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedBack(int id, [FromBody] UpdateFeedBackRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _feedBackService.UpdateFeedBackAsync(id, request);
            if (result.StatusCode == StatusCodeEnum.OK_200)
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedBack(int id)
        {
            var result = await _feedBackService.DeleteFeedBackAsync(id);
            if (result.StatusCode == StatusCodeEnum.OK_200)
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, result);
        }
    }
} 