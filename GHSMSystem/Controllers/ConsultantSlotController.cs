using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.Enums;

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

        [HttpPost("register")]
        public async Task<IActionResult> Register(
        [FromQuery] string consultantId,
        [FromQuery] int slotId)
        {
            var res = await _consultantSlotService.RegisterAsync(consultantId, slotId);
            return res.StatusCode switch
            {
                StatusCodeEnum.Created_201 => Created("", res),
                StatusCodeEnum.Conflict_409 => Conflict(res),
                StatusCodeEnum.NotFound_404 => NotFound(res),
                _ => StatusCode(500, res)
            };
        }

        
        [HttpPut("swap")]
        public async Task<IActionResult> Swap(
            [FromQuery] string consultantA, [FromQuery] int slotA,
            [FromQuery] string consultantB, [FromQuery] int slotB)
        {
            var res = await _consultantSlotService.SwapAsync(consultantA, slotA, consultantB, slotB);
            return res.StatusCode switch
            {
                StatusCodeEnum.OK_200 => Ok(res),
                StatusCodeEnum.Conflict_409 => Conflict(res),
                StatusCodeEnum.NotFound_404 => NotFound(res),
                _ => StatusCode(500, res)
            };
        }
    }
}
