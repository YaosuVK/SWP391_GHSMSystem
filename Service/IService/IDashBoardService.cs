using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Response.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IDashBoardService
    {
        Task<BaseResponse<List<GetTotalAppointmentsTotalAppointmentsAmount>>> 
            GetTotalAppointmentsTotalAppointmentsAmountAsync
            (DateTime startDate, DateTime endDate, string? timeSpanType);

        Task<BaseResponse<GetTotalAppointmentsAndAmount>> GetTotalAppointmentsAndAmount();

        Task<BaseResponse<List<GetCurrentWeekRevenue>>> GetCurrentWeekRevenue();
    }
}
