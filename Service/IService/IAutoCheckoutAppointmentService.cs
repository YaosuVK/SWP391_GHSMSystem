using Service.RequestAndResponse.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAutoCheckoutAppointmentService
    {
        Task<BaseResponse<int>> CancelExpiredAppointments();
        Task<BaseResponse<string>> AutoCheckOutAppointments();
    }
}
