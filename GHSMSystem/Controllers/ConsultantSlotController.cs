using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.ConsultantProfiles;
using Service.Service;
using System.Security.Claims;

namespace GHSMSystem.Controllers
{
    [Route("api/consultantSlot")]
    [ApiController]
    public class ConsultantSlotController : ControllerBase
    {
        private readonly IConsultantSlotService _consultantSlotService;
        private readonly IConsultantProfileServive _consultantProfileServive;

        public ConsultantSlotController(IConsultantSlotService consultantSlotService, IConsultantProfileServive consultantProfileServive)
        {
            _consultantSlotService = consultantSlotService;
            _consultantProfileServive = consultantProfileServive;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _consultantSlotService.GetAllAsync();
            return res.StatusCode switch
            {
                StatusCodeEnum.OK_200 => Ok(res),
                StatusCodeEnum.NotFound_404 => NotFound(res),
                _ => StatusCode(500, res)
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetRegistered(
        [FromQuery] string consultantId)
        {
            var res = await _consultantSlotService.GetRegisteredSlotsAsync(consultantId);
            return res.StatusCode switch
            {
                StatusCodeEnum.OK_200 => Ok(res),
                StatusCodeEnum.NotFound_404 => NotFound(res),
                _ => StatusCode(500, res)
            };
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchConsultantSlots(
        [FromQuery] string? keyword,
        [FromQuery] DateTime? date)
        {
            var response = await _consultantSlotService.SearchAsync(keyword, date);

            return response.StatusCode switch
            {
                StatusCodeEnum.OK_200 => Ok(response),
                StatusCodeEnum.NotFound_404 => NotFound(response),
                _ => StatusCode(500, response)
            };
        }

        [HttpPost("register")]
        [Authorize(Roles = "Consultant")]
        public async Task<IActionResult> Register(
        [FromQuery] int slotId,
        [FromQuery] int maxAppointment)
        {
            // đọc đúng claim bạn đã gán khi tạo token
            var consultantId = User.FindFirstValue("AccountID");
            if (string.IsNullOrEmpty(consultantId))
                return Unauthorized(new { message = "Invalid token: AccountID missing" });

            var res = await _consultantSlotService.RegisterAsync(consultantId, slotId, maxAppointment);
            return res.StatusCode switch
            {
                StatusCodeEnum.Created_201 => CreatedAtAction(nameof(GetRegistered), new { consultantId }, res),
                StatusCodeEnum.Conflict_409 => Conflict(res),
                StatusCodeEnum.NotFound_404 => NotFound(res),
                _ => StatusCode(500, res)
            };
        }

        [HttpPost("CreateConsultantProfile")]
        public async Task<ActionResult<BaseResponse<CreateConsultantProfile>>> CreateConsultantProfile(CreateConsultantProfile request)
        {
            if (request == null)
            {
                return BadRequest("Please Implement all Information");
            }

            if (!ModelState.IsValid)
            {
                // Trả về lỗi chi tiết từ ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { message = "Validation Failed", errors });
            }

            var consultantProfile = await _consultantProfileServive.CreateConsultantProfile(request);
            return Ok(consultantProfile);
        }

        [HttpPut("UpdateConsultantProfile")]
        public async Task<ActionResult<BaseResponse<UpdateConsultantProfile>>> UpdateConsultantProfile(int consultantProfileID, UpdateConsultantProfile request)
        {
            if (request == null)
            {
                return BadRequest("Please Implement all Information");
            }

            if (!ModelState.IsValid)
            {
                // Trả về lỗi chi tiết từ ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { message = "Validation Failed", errors });
            }

            var consultantProfile = await _consultantProfileServive.UpdateConsultantProfile(consultantProfileID, request);
            return Ok(consultantProfile);
        }

        [HttpPut("swap")]
        public async Task<IActionResult> Swap(
        [FromQuery] string consultantA,
        [FromQuery] int slotA,
        [FromQuery] string consultantB,
        [FromQuery] int slotB)
        {
            var res = await _consultantSlotService.SwapAsync(consultantA, slotA, consultantB, slotB);
            return res.StatusCode switch
            {
                StatusCodeEnum.OK_200 => Ok(res),
                StatusCodeEnum.NotFound_404 => NotFound(res),
                _ => StatusCode(500, res)
            };
        }
    }
}
