using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.ConsultantProfiles;
using Service.RequestAndResponse.Response.ConsultantProfiles;
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

        [HttpGet("GetAllConsultantSlot")]
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

        [HttpGet("GetAllConsultantProfile")]
        public async Task<ActionResult<BaseResponse<IEnumerable<GetAllConsultantProfile?>>>> GetAllConsultantProfile()
        {
            var consultantProfile = await _consultantProfileServive.GetAllConsultantProfile();
            return Ok(consultantProfile);
        }

        [HttpGet("GetConsultantProfileByAccountId/{accountId}")]
        public async Task<ActionResult<BaseResponse<GetConsultantProfileResponse?>>> GetConsultantProfileByAccountID(string accountId)
        {
            var consultantProfile = await _consultantProfileServive.GetConsultantProfileByAccountID(accountId);
            return Ok(consultantProfile);
        }

        [HttpGet("GetConsultantProfileById/{consultantProfileID}")]
        public async Task<ActionResult<BaseResponse<GetConsultantProfileResponse?>>> GetConsultantProfileByID(int? consultantProfileID)
        {
            var consultantProfile = await _consultantProfileServive.GetConsultantProfileByID(consultantProfileID);
            return Ok(consultantProfile);
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
        //[Authorize(Roles = "Consultant,Manager")]
        public async Task<IActionResult> Register(
        [FromQuery] int slotId,
        [FromQuery] string? consultantId = null,
        [FromQuery] int? maxAppointment = null)
        {
            // Đọc AccountID từ token nếu có
            var currentUserId = User.FindFirstValue("AccountID");

            // Xác định ID để sử dụng
            string idToUse = consultantId;

            // Nếu consultantId không được cung cấp, hãy thử lấy nó từ token
            if (string.IsNullOrEmpty(idToUse))
            {
                idToUse = currentUserId;
            }

            // Nếu sau cả hai bước vẫn không có ID, trả về lỗi
            if (string.IsNullOrEmpty(idToUse))
            {
                return Unauthorized(new { message = "Invalid token: AccountID missing or ConsultantID is required" });
            }

            int finalMaxAppointment;

            var userRoles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            if (userRoles.Contains("Manager"))
            {
                // Manager must provide consultantId to register for a consultant
                if (string.IsNullOrEmpty(consultantId))
                {
                    return BadRequest(new { message = "Manager must provide consultantId parameter to register slot for consultant" });
                }
                idToUse = consultantId;
                finalMaxAppointment = maxAppointment ?? 10;
            }
            else if (userRoles.Contains("Consultant"))
            {
                // Consultant can only register for themselves
                if (!string.IsNullOrEmpty(consultantId) && consultantId != currentUserId)
                {
                    return Forbid("Consultant can only register slots for themselves");
                }
                idToUse = currentUserId;
                finalMaxAppointment = maxAppointment ?? 10;
            }
            // If user has no role but a valid ID was passed (e.g. for anonymous registration)
            else if (userRoles.Count == 0)
            {
                finalMaxAppointment = maxAppointment ?? 10; // Default value
            }
            else
            {
                return Forbid("User does not have the required role to register for slots.");
            }

            var res = await _consultantSlotService.RegisterAsync(idToUse, slotId, finalMaxAppointment);
            return res.StatusCode switch
            {
                StatusCodeEnum.Created_201 => CreatedAtAction(nameof(GetRegistered), new { consultantId = idToUse }, res),
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

        [HttpPut("updateMaxAppointment")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateMaxAppointment(
        [FromQuery] string consultantId,
        [FromQuery] int slotId,
        [FromQuery] int newMaxAppointment)
        {
            var res = await _consultantSlotService.UpdateMaxAppointmentAsync(consultantId, slotId, newMaxAppointment);
            return res.StatusCode switch
            {
                StatusCodeEnum.OK_200 => Ok(res),
                StatusCodeEnum.NotFound_404 => NotFound(res),
                StatusCodeEnum.BadRequest_400 => BadRequest(res),
                _ => StatusCode(500, res)
            };
        }

        [HttpPut("updateConsultantPrice")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateConsultantPrice(
        [FromQuery] int consultantProfileId,
        [FromQuery] double newPrice)
        {
            var res = await _consultantProfileServive.UpdateConsultantPriceAsync(consultantProfileId, newPrice);
            return res.StatusCode switch
            {
                StatusCodeEnum.OK_200 => Ok(res),
                StatusCodeEnum.NotFound_404 => NotFound(res),
                StatusCodeEnum.BadRequest_400 => BadRequest(res),
                _ => StatusCode(500, res)
            };
        }
    }
}
