using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.Request.MenstrualCycles;
using System;
using System.Threading.Tasks;

namespace GHSMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenstrualCycleController : ControllerBase
    {
        private readonly IMenstrualCycleService _menstrualCycleService;

        public MenstrualCycleController(IMenstrualCycleService menstrualCycleService)
        {
            _menstrualCycleService = menstrualCycleService;
        }

        /// <summary>
        /// Get all menstrual cycles
        /// </summary>
        /// <returns>List of all menstrual cycles</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllMenstrualCycles()
        {
            var result = await _menstrualCycleService.GetAllMenstrualCyclesAsync();
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get menstrual cycle by ID
        /// </summary>
        /// <param name="id">Menstrual cycle ID</param>
        /// <returns>Menstrual cycle details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenstrualCycleById(int id)
        {
            var result = await _menstrualCycleService.GetMenstrualCycleByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get menstrual cycles by customer ID
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>List of menstrual cycles for the customer</returns>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetMenstrualCyclesByCustomerId(string customerId)
        {
            var result = await _menstrualCycleService.GetMenstrualCyclesByCustomerIdAsync(customerId);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get menstrual cycles by date range
        /// </summary>
        /// <param name="fromDate">Start date</param>
        /// <param name="toDate">End date</param>
        /// <returns>List of menstrual cycles within the date range</returns>
        [HttpGet("date-range")]
        public async Task<IActionResult> GetMenstrualCyclesByDateRange([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var result = await _menstrualCycleService.GetMenstrualCyclesByDateRangeAsync(fromDate, toDate);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Search menstrual cycles with pagination
        /// </summary>
        /// <param name="search">Search term (optional)</param>
        /// <param name="pageIndex">Page index (default: 1)</param>
        /// <param name="pageSize">Page size (default: 10)</param>
        /// <returns>Paginated list of menstrual cycles</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchMenstrualCycles([FromQuery] string? search, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _menstrualCycleService.SearchMenstrualCyclesAsync(search, pageIndex, pageSize);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Create a new menstrual cycle
        /// </summary>
        /// <param name="request">Menstrual cycle creation request</param>
        /// <returns>Created menstrual cycle</returns>
        [HttpPost]
        public async Task<IActionResult> CreateMenstrualCycle([FromBody] CreateMenstrualCycleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _menstrualCycleService.CreateMenstrualCycleAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Update an existing menstrual cycle
        /// </summary>
        /// <param name="request">Menstrual cycle update request</param>
        /// <returns>Updated menstrual cycle</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateMenstrualCycle([FromBody] UpdateMenstrualCycleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _menstrualCycleService.UpdateMenstrualCycleAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Delete a menstrual cycle
        /// </summary>
        /// <param name="id">Menstrual cycle ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenstrualCycle(int id)
        {
            var result = await _menstrualCycleService.DeleteMenstrualCycleAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
} 