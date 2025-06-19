using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Slot;
using Service.RequestAndResponse.Response.Slots;

namespace GHSMSystem.Controllers
{
    [Route("api/slot")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly ISlotService _slotService;
        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet]
        [Route("GetSlot")]
        public async Task<IActionResult> GetAllSlot()
        {
            var response = await _slotService.GetAllAsync();
            return response.StatusCode == StatusCodeEnum.OK_200
                ? Ok(response)
                : StatusCode(502, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlot([FromBody] CreateSlotRequest request)
        {
            var response = await _slotService.AddAsync(request);
            return response.StatusCode switch
            {
                StatusCodeEnum.Created_201 => CreatedAtAction(nameof(GetAllSlot), response),
                StatusCodeEnum.BadRequest_400 => BadRequest(response),
                StatusCodeEnum.NotFound_404 => NotFound(response),
                StatusCodeEnum.Conflict_409 => Conflict(response),
                StatusCodeEnum.NotAcceptable_406 => StatusCode(406, response),
                _ => StatusCode(500, response)
            };
        }




        [HttpPut]
        public async Task<IActionResult> UpdateSlot(int slotId, [FromBody] UpdateSlotRequest request)
        {
            var response = await _slotService.UpdateAsync(slotId, request);
            return response.StatusCode switch
            {
                StatusCodeEnum.OK_200 => Ok(response),
                StatusCodeEnum.BadRequest_400 => BadRequest(response),
                StatusCodeEnum.NotFound_404 => NotFound(response),
                StatusCodeEnum.NotAcceptable_406 => StatusCode(406, response),
                _ => StatusCode(500, response)
            };
        }

        [HttpDelete("{slotId:int}")]
        public async Task<IActionResult> DeleteSlot(int slotId)
        {
            var response = await _slotService.DeleteAsync(slotId);
            return response.StatusCode == StatusCodeEnum.OK_200
                ? NoContent()
                : NotFound(response);
        }
    }
}
