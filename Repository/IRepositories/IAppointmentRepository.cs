using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllAppointment();
        Task<Appointment?> GetAppointmentByIdAsync(int appointmentId);
        Task<Appointment?> GetAppointmentByIdCanNullAsync(int? appointmentId);
        Task<Appointment?> GetAppointmentByCodeAsync(string appointmentCode);
        Task<IEnumerable<Appointment>> GetAppointmentsByCustomerId(string accountId);
        Task<IEnumerable<Appointment>> GetAppointmentsByConsultantId(string accountId);
        Task<Appointment?> GetUnpaidAppointmentByID(string customerID);
        Task AddAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task<bool> ExistsAppointmentCodeAsync(string code);
        Task<IEnumerable<Appointment>> GetExpiredAppointmentsAsync();
        Task<IEnumerable<Appointment>> GetCheckOutAppointmentsAsync();
        Task<Appointment?> ChangeAppointmentStatus(int appointmentId, AppointmentStatus status, PaymentStatus paymentStatus);
        Task<(int appointmentsReturnOrCancell, int appointments, int appointmentsComplete, int appointmentsCancell, int appointmentsReturnRefund, int appointmentsReport, int appointmentConfirmed)> GetStaticAppointments();

    }
}
