using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Response.DashBoard;

namespace GHSMSystem.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;
        public DashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("adminDashBoard/GetTotalAppointmentsTotalAppointmentsAmount")]
        public async Task<BaseResponse<List<GetTotalAppointmentsTotalAppointmentsAmount>>> GetTotalAppointmentsTotalAppointmentsAmountAsync
            (DateTime startDate, DateTime endDate, string? timeSpanType)
        {
            return await _dashBoardService.GetTotalAppointmentsTotalAppointmentsAmountAsync(startDate, endDate, timeSpanType);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("adminDashBoard/GetCurrentWeekRevenue")]
        public async Task<BaseResponse<List<GetCurrentWeekRevenue>>> GetCurrentWeekRevenue()
        {
            return await _dashBoardService.GetCurrentWeekRevenue();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("adminDashBoard/GetTotalAppointmentsAndAmount")]
        public async Task<BaseResponse<GetTotalAppointmentsAndAmount>> GetTotalAppointmentsAndAmount()
        {
            return await _dashBoardService.GetTotalAppointmentsAndAmount();
        }
    }
}
