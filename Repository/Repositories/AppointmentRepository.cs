using BusinessObject.Model;
using DataAccessObject;
using Repository.BaseRepository;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly AppointmentDAO _appointmentDao;
        public AppointmentRepository(AppointmentDAO appointmentDao) : base(appointmentDao)
        {
            _appointmentDao = appointmentDao;
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await _appointmentDao.AddAppointmentAsync(appointment);
        }

        public async Task<Appointment?> ChangeAppointmentStatus(int appointmentId, AppointmentStatus status, PaymentStatus paymentStatus)
        {
            return await _appointmentDao.ChangeAppointmentStatus(appointmentId, status, paymentStatus);
        }

        public async Task<bool> ExistsAppointmentCodeAsync(string code)
        {
            return await _appointmentDao.ExistsAppointmentCodeAsync(code);
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointment()
        {
            return await _appointmentDao.GetAllAppointment();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _appointmentDao.GetAppointmentByIdAsync(appointmentId);
        }

        public async Task<Appointment?> GetAppointmentByIdCanNullAsync(int? appointmentId)
        {
            return await _appointmentDao.GetAppointmentByIdCanNullAsync(appointmentId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByConsultantId(string accountId)
        {
            return await _appointmentDao.GetAppointmentsByConsultantId(accountId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByCustomerId(string accountId)
        {
            return await _appointmentDao.GetAppointmentsByCustomerId(accountId);
        }

        public async Task<(int appointmentsReturnOrCancell, int appointments, int appointmentsComplete, int appointmentsCancell, int appointmentsReturnRefund, int appointmentsReport, int appointmentConfirmed)> GetStaticAppointments()
        {
            return await _appointmentDao.GetStaticAppointments();
        }

        public async Task<Appointment?> GetUnpaidAppointmentByID(string customerID)
        {
            return await _appointmentDao.GetUnpaidAppointmentByID(customerID);
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
           await _appointmentDao.UpdateAsync(appointment);
        }
    }
}
