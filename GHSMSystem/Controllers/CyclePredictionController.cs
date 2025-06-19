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

        /// <summary>
        /// Get all cycle predictions
        /// </summary>
        /// <returns>List of all cycle predictions</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCyclePredictions()
        {
            var result = await _cyclePredictionService.GetAllCyclePredictionsAsync();
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get cycle prediction by ID
        /// </summary>
        /// <param name="id">Cycle prediction ID</param>
        /// <returns>Cycle prediction details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCyclePredictionById(int id)
        {
            var result = await _cyclePredictionService.GetCyclePredictionByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get cycle predictions by menstrual cycle ID
        /// </summary>
        /// <param name="menstrualCycleId">Menstrual cycle ID</param>
        /// <returns>List of cycle predictions for the menstrual cycle</returns>
        [HttpGet("menstrual-cycle/{menstrualCycleId}")]
        public async Task<IActionResult> GetCyclePredictionsByMenstrualCycleId(int menstrualCycleId)
        {
            var result = await _cyclePredictionService.GetCyclePredictionsByMenstrualCycleIdAsync(menstrualCycleId);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get cycle predictions by customer ID
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>List of cycle predictions for the customer</returns>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCyclePredictionsByCustomerId(string customerId)
        {
            var result = await _cyclePredictionService.GetCyclePredictionsByCustomerIdAsync(customerId);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get cycle predictions by date range
        /// </summary>
        /// <param name="fromDate">Start date</param>
        /// <param name="toDate">End date</param>
        /// <returns>List of cycle predictions within the date range</returns>
        [HttpGet("date-range")]
        public async Task<IActionResult> GetCyclePredictionsByDateRange([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var result = await _cyclePredictionService.GetCyclePredictionsByDateRangeAsync(fromDate, toDate);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Search cycle predictions with pagination
        /// </summary>
        /// <param name="search">Search term (optional)</param>
        /// <param name="pageIndex">Page index (default: 1)</param>
        /// <param name="pageSize">Page size (default: 10)</param>
        /// <returns>Paginated list of cycle predictions</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchCyclePredictions([FromQuery] string? search, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _cyclePredictionService.SearchCyclePredictionsAsync(search, pageIndex, pageSize);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Generate cycle prediction automatically based on menstrual cycle data
        /// </summary>
        /// <param name="menstrualCycleId">Menstrual cycle ID</param>
        /// <returns>Generated cycle prediction</returns>
        [HttpPost("generate/{menstrualCycleId}")]
        public async Task<IActionResult> GenerateCyclePrediction(int menstrualCycleId)
        {
            var result = await _cyclePredictionService.GenerateCyclePredictionAsync(menstrualCycleId);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Create a new cycle prediction
        /// </summary>
        /// <param name="request">Cycle prediction creation request</param>
        /// <returns>Created cycle prediction</returns>
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

        /// <summary>
        /// Update an existing cycle prediction
        /// </summary>
        /// <param name="request">Cycle prediction update request</param>
        /// <returns>Updated cycle prediction</returns>
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

        /// <summary>
        /// Delete a cycle prediction
        /// </summary>
        /// <param name="id">Cycle prediction ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCyclePrediction(int id)
        {
            var result = await _cyclePredictionService.DeleteCyclePredictionAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
} 