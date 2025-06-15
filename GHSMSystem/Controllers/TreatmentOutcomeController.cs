using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.Request.TreatmentOutcomes;
using System;
using System.Threading.Tasks;

namespace GHSMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentOutcomeController : ControllerBase
    {
        private readonly ITreatmentOutcomeService _treatmentOutcomeService;

        public TreatmentOutcomeController(ITreatmentOutcomeService treatmentOutcomeService)
        {
            _treatmentOutcomeService = treatmentOutcomeService;
        }

        /// <summary>
        /// Get all treatment outcomes
        /// </summary>
        /// <returns>List of all treatment outcomes</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTreatmentOutcomes()
        {
            var result = await _treatmentOutcomeService.GetAllTreatmentOutcomesAsync();
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get treatment outcome by ID
        /// </summary>
        /// <param name="id">Treatment outcome ID</param>
        /// <returns>Treatment outcome details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTreatmentOutcomeById(int id)
        {
            var result = await _treatmentOutcomeService.GetTreatmentOutcomeByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get treatment outcomes by customer ID
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>List of treatment outcomes for the customer</returns>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetTreatmentOutcomesByCustomerId(string customerId)
        {
            var result = await _treatmentOutcomeService.GetTreatmentOutcomesByCustomerIdAsync(customerId);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get treatment outcomes by consultant ID
        /// </summary>
        /// <param name="consultantId">Consultant ID</param>
        /// <returns>List of treatment outcomes for the consultant</returns>
        [HttpGet("consultant/{consultantId}")]
        public async Task<IActionResult> GetTreatmentOutcomesByConsultantId(string consultantId)
        {
            var result = await _treatmentOutcomeService.GetTreatmentOutcomesByConsultantIdAsync(consultantId);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get treatment outcomes by appointment ID
        /// </summary>
        /// <param name="appointmentId">Appointment ID</param>
        /// <returns>List of treatment outcomes for the appointment</returns>
        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetTreatmentOutcomesByAppointmentId(int appointmentId)
        {
            var result = await _treatmentOutcomeService.GetTreatmentOutcomesByAppointmentIdAsync(appointmentId);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Search treatment outcomes with pagination
        /// </summary>
        /// <param name="search">Search term (optional)</param>
        /// <param name="pageIndex">Page index (default: 1)</param>
        /// <param name="pageSize">Page size (default: 10)</param>
        /// <returns>Paginated list of treatment outcomes</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchTreatmentOutcomes([FromQuery] string? search, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _treatmentOutcomeService.SearchTreatmentOutcomesAsync(search, pageIndex, pageSize);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Create a new treatment outcome
        /// </summary>
        /// <param name="request">Treatment outcome creation request</param>
        /// <returns>Created treatment outcome</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTreatmentOutcome([FromBody] CreateTreatmentOutcomeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _treatmentOutcomeService.CreateTreatmentOutcomeAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Update an existing treatment outcome
        /// </summary>
        /// <param name="request">Treatment outcome update request</param>
        /// <returns>Updated treatment outcome</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTreatmentOutcome([FromBody] UpdateTreatmentOutcomeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _treatmentOutcomeService.UpdateTreatmentOutcomeAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Delete a treatment outcome
        /// </summary>
        /// <param name="id">Treatment outcome ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatmentOutcome(int id)
        {
            var result = await _treatmentOutcomeService.DeleteTreatmentOutcomeAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
} 