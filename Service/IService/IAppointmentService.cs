using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAppointmentService
    {
        Task<BaseResponse<IEnumerable<Appointment>>> GetAllAppointment();
        Task<BaseResponse<Appointment?>> GetAppointmentByIdAsync(int appointmentId);
        Task<BaseResponse<Appointment?>> GetAppointmentByIdCanNullAsync(int? appointmentId);
        Task<BaseResponse<IEnumerable<Appointment>>> GetAppointmentsByCustomerId(string accountId);
        Task<BaseResponse<IEnumerable<Appointment>>> GetAppointmentsByConsultantId(string accountId);
        Task AddAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
    }
}
