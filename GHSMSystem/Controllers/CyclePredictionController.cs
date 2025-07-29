using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.Request.CyclePredictions;
using System;
using System.Threading.Tasks;

namespace GHSMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CyclePredictionController : ControllerBase
    {
        private readonly ICyclePredictionService _cyclePredictionService;

        public CyclePredictionController(ICyclePredictionService cyclePredictionService)
        {
            _cyclePredictionService = cyclePredictionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCyclePredictions()
        {
            var result = await _cyclePredictionService.GetAllCyclePredictionsAsync();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCyclePredictionById(int id)
        {
            var result = await _cyclePredictionService.GetCyclePredictionByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("menstrual-cycle/{menstrualCycleId}")]
        public async Task<IActionResult> GetCyclePredictionsByMenstrualCycleId(int menstrualCycleId)
        {
            var result = await _cyclePredictionService.GetCyclePredictionsByMenstrualCycleIdAsync(menstrualCycleId);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCyclePredictionsByCustomerId(string customerId)
        {
            var result = await _cyclePredictionService.GetCyclePredictionsByCustomerIdAsync(customerId);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("date-range")]
        public async Task<IActionResult> GetCyclePredictionsByDateRange([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var result = await _cyclePredictionService.GetCyclePredictionsByDateRangeAsync(fromDate, toDate);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchCyclePredictions([FromQuery] string? search, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _cyclePredictionService.SearchCyclePredictionsAsync(search, pageIndex, pageSize);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("generate/{menstrualCycleId}")]
        public async Task<IActionResult> GenerateCyclePrediction(int menstrualCycleId)
        {
            var result = await _cyclePredictionService.GenerateCyclePredictionAsync(menstrualCycleId);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateCyclePrediction([FromBody] CreateCyclePredictionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cyclePredictionService.CreateCyclePredictionAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut]
        public async Task<IActionResult> UpdateCyclePrediction([FromBody] UpdateCyclePredictionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cyclePredictionService.UpdateCyclePredictionAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCyclePrediction(int id)
        {
            var result = await _cyclePredictionService.DeleteCyclePredictionAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
} 