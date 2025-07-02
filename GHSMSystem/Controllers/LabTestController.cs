using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.Request.LabTests;
using System;
using System.Threading.Tasks;

namespace GHSMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestController : ControllerBase
    {
        private readonly ILabTestService _labTestService;

        public LabTestController(ILabTestService labTestService)
        {
            _labTestService = labTestService;
        }

        /// <summary>
        /// Get all lab tests
        /// </summary>
        /// <returns>List of all lab tests</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllLabTests()
        {
            var result = await _labTestService.GetAllLabTestsAsync();
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get lab test by ID
        /// </summary>
        /// <param name="id">Lab test ID</param>
        /// <returns>Lab test details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLabTestById(int id)
        {
            var result = await _labTestService.GetLabTestByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get lab tests by customer ID
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>List of lab tests for the customer</returns>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetLabTestsByCustomerId(string customerId)
        {
            var result = await _labTestService.GetLabTestsByCustomerIdAsync(customerId);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get lab tests by staff ID
        /// </summary>
        /// <param name="staffId">Staff ID</param>
        /// <returns>List of lab tests for the staff</returns>
        [HttpGet("staff/{staffId}")]
        public async Task<IActionResult> GetLabTestsByStaffId(string staffId)
        {
            var result = await _labTestService.GetLabTestsByStaffIdAsync(staffId);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get lab tests by treatment ID
        /// </summary>
        /// <param name="treatmentId">Treatment ID</param>
        /// <returns>List of lab tests for the treatment</returns>
        [HttpGet("treatment/{treatmentId}")]
        public async Task<IActionResult> GetLabTestsByTreatmentId(int treatmentId)
        {
            var result = await _labTestService.GetLabTestsByTreatmentIdAsync(treatmentId);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get lab tests by date range
        /// </summary>
        /// <param name="fromDate">Start date</param>
        /// <param name="toDate">End date</param>
        /// <returns>List of lab tests within the date range</returns>
        [HttpGet("date-range")]
        public async Task<IActionResult> GetLabTestsByDateRange([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var result = await _labTestService.GetLabTestsByDateRangeAsync(fromDate, toDate);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Search lab tests with pagination
        /// </summary>
        /// <param name="search">Search term (optional)</param>
        /// <param name="pageIndex">Page index (default: 1)</param>
        /// <param name="pageSize">Page size (default: 10)</param>
        /// <returns>Paginated list of lab tests</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchLabTests([FromQuery] string? search, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _labTestService.SearchLabTestsAsync(search, pageIndex, pageSize);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Create a new lab test
        /// </summary>
        /// <param name="request">Lab test creation request</param>
        /// <returns>Created lab test</returns>
        [HttpPost]
        public async Task<IActionResult> CreateLabTest([FromBody] CreateLabTestRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _labTestService.CreateLabTestAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Update an existing lab test
        /// </summary>
        /// <param name="request">Lab test update request</param>
        /// <returns>Updated lab test</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateLabTest([FromBody] UpdateLabTestRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _labTestService.UpdateLabTestAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Delete a lab test
        /// </summary>
        /// <param name="id">Lab test ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabTest(int id)
        {
            var result = await _labTestService.DeleteLabTestAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
} 