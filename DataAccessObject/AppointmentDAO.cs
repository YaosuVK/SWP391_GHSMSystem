using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class AppointmentDAO : BaseDAO<Appointment>
    {
        private readonly GHSMContext _context;
        
        public AppointmentDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointment()
        {
            IQueryable<Appointment> appointments = _context.Appointments
                .Include(b => b.AppointmentDetails)
                .Include(b => b.Customer)
                .Include(b => b.Consultant)
                .Include(b => b.FeedBacks)
                .Include(b => b.Slot);

            return await appointments.ToListAsync();
        }

        public async Task<Appointment?> GetUnpaidAppointmentByID(string customerID)
        {
            return await _context.Appointments
                .Include(a => a.AppointmentDetails)
                .FirstOrDefaultAsync(a => a.CustomerID == customerID && a.Status == AppointmentStatus.Pending);
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments
                .Include(b => b.AppointmentDetails)
                .ThenInclude(b => b.Service)
                .Include(b => b.Customer)
                .Include(b => b.Consultant)
                .Include(b => b.FeedBacks)
                .Include(b => b.Slot)
                .FirstOrDefaultAsync(o => o.AppointmentID == appointmentId);
        }

        public async Task<Appointment?> GetAppointmentByIdCanNullAsync(int? appointmentId)
        {
            return await _context.Appointments
                .Include(b => b.AppointmentDetails)
                .ThenInclude(b => b.Service)
                .Include(b => b.Customer)
                .Include(b => b.Consultant)
                .Include(b => b.FeedBacks)
                .Include(b => b.Slot)
                .FirstOrDefaultAsync(o => o.AppointmentID == appointmentId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByCustomerId(string accountId)
        {
            return await _context.Appointments
                 .Include(b => b.AppointmentDetails)
                 .ThenInclude(b => b.Service)
                 .Include(b => b.Customer)
                 .Include(b => b.Consultant)
                 .Include(b => b.FeedBacks)
                 .Include(b => b.Slot)
                 .Where(b => b.CustomerID == accountId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByConsultantId(string accountId)
        {
            return await _context.Appointments
                 .Include(b => b.AppointmentDetails)
                 .ThenInclude(b => b.Service)
                 .Include(b => b.Customer)
                 .Include(b => b.Consultant)
                 .Include(b => b.FeedBacks)
                 .Include(b => b.Slot)
                 .Where(b => b.ConsultantID == accountId)
                 .ToListAsync();
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        /*public async Task<Appointment?> UpdateBookingWithFeedBackAsync(int bookingId, Appointment booking)
        {

            var existBooking = await _context.Appointments.FindAsync(bookingId);
            if (existBooking != null)
            {
                existBooking.FeedBacks. = booking.ReportID;
            }
            await _context.SaveChangesAsync();
            return existBooking;
        }*/

        public async Task<(int appointmentsReturnOrCancell, int appointments, int appointmentsComplete, int appointmentsCancell, int appointmentsReturnRefund, int appointmentsReport, int appointmentConfirmed)> GetStaticAppointments()
        {
            /*// Lấy ngày hiện tại
            DateTime today = DateTime.Today;

            // Xác định ngày đầu tuần (ngày thứ Hai là ngày đầu tuần)
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

            // Xác định ngày cuối tuần (ngày Chủ nhật là ngày cuối tuần)
            DateTime endOfWeek = startOfWeek.AddDays(6);*/

            int appointmentsReturnOrCancell = await _context.Appointments
                 .Where(o => o.Status == AppointmentStatus.Cancelled
                          || o.paymentStatus == PaymentStatus.Refunded)
                 .CountAsync();

            int appointments = await _context.Appointments
                                .CountAsync();

            int appointmentsComplete = await _context.Appointments
                                .Where(o => o.Status == AppointmentStatus.Completed)
                                .CountAsync();

            int appointmentsCancell = await _context.Appointments
                                .Where(o => o.Status == AppointmentStatus.Cancelled && o.paymentStatus != PaymentStatus.Refunded)
                                .CountAsync();

            int appointmentsReturnRefund = await _context.Appointments
                                .Where(o => o.paymentStatus == PaymentStatus.Refunded)
                                .CountAsync();

            int appointmentsReport = await _context.Appointments
                                .Where(o => o.FeedBacks != null)
                                .CountAsync();

            int appointmentConfirmed = await _context.Appointments
                                .Where(o => o.Status == AppointmentStatus.Confirmed && (o.paymentStatus == PaymentStatus.Deposited || o.paymentStatus == PaymentStatus.FullyPaid))
                                .CountAsync();
            return (appointmentsReturnOrCancell, appointments, appointmentsComplete, appointmentsCancell, appointmentsReturnRefund, appointmentsReport, appointmentConfirmed);
        }

        public async Task<Appointment?> ChangeAppointmentStatus(int appointmentId, AppointmentStatus status, PaymentStatus paymentStatus)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.Status = status;
                appointment.paymentStatus = paymentStatus;
                await _context.SaveChangesAsync();
            }

            return await _context.Appointments.FindAsync(appointmentId);
        }


    }
}
