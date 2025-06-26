using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.Enums;
using System.Security.Claims;

namespace GHSMSystem.Controllers
{
    [Route("api/consultantSlot")]
    [ApiController]
    public class ConsultantSlotController : ControllerBase
    {
        private readonly IConsultantSlotService _consultantSlotService;

        public ConsultantSlotController(IConsultantSlotService consultantSlotService)
        {
            _consultantSlotService = consultantSlotService;
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
