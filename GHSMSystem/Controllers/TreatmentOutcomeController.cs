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

        [HttpGet]
        public async Task<IActionResult> GetAllTreatmentOutcomes()
        {
            var result = await _treatmentOutcomeService.GetAllTreatmentOutcomesAsync();
            return StatusCode((int)result.StatusCode, result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTreatmentOutcomeById(int id)
        {
            var result = await _treatmentOutcomeService.GetTreatmentOutcomeByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }
        
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetTreatmentOutcomesByCustomerId(string customerId)
        {
            var result = await _treatmentOutcomeService.GetTreatmentOutcomesByCustomerIdAsync(customerId);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("consultant/{consultantId}")]
        public async Task<IActionResult> GetTreatmentOutcomesByConsultantId(string consultantId)
        {
            var result = await _treatmentOutcomeService.GetTreatmentOutcomesByConsultantIdAsync(consultantId);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetTreatmentOutcomesByAppointmentId(int appointmentId)
        {
            var result = await _treatmentOutcomeService.GetTreatmentOutcomesByAppointmentIdAsync(appointmentId);
            return StatusCode((int)result.StatusCode, result);
        }
        
        [HttpGet("search")]
        public async Task<IActionResult> SearchTreatmentOutcomes([FromQuery] string? search, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _treatmentOutcomeService.SearchTreatmentOutcomesAsync(search, pageIndex, pageSize);
            return StatusCode((int)result.StatusCode, result);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateTreatmentOutcome([FromBody] CreateTreatmentOutcomeRequest request)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { message = "Validation Failed", errors });
            }

            var result = await _treatmentOutcomeService.CreateTreatmentOutcomeAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }

        
        [HttpPut]
        public async Task<IActionResult> UpdateTreatmentOutcome([FromBody] UpdateTreatmentOutcomeRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { message = "Validation Failed", errors });
            }

            var result = await _treatmentOutcomeService.UpdateTreatmentOutcomeAsync(request);
            return StatusCode((int)result.StatusCode, result);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatmentOutcome(int id)
        {
            var result = await _treatmentOutcomeService.DeleteTreatmentOutcomeAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
} 