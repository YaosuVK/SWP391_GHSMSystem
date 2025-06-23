using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Appointments;
using Service.RequestAndResponse.Response.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAppointmentService
    {
        Task<BaseResponse<IEnumerable<GetAllAppointment>>> GetAllAppointment();
        Task<BaseResponse<GetAllAppointment?>> GetAppointmentByIdAsync(int appointmentId);
        /*Task<BaseResponse<GetAllAppointment?>> GetAppointmentByIdCanNullAsync(int? appointmentId);*/
        Task<BaseResponse<IEnumerable<GetAllAppointment>>> GetAppointmentsByCustomerId(string accountId);
        Task<BaseResponse<IEnumerable<GetAllAppointment>>> GetAppointmentsByConsultantId(string accountId);
        Task<BaseResponse<int>> CreateAppointment(CreateAppointmentRequest request);
        Task<BaseResponse<UpdateAppointmentRequest>> UpdateAppointment(int appointmentID,UpdateAppointmentRequest appointment);
    }
}
