using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IDashBoardRepository : IBaseRepository<Appointment>
    {
        Task<List<(object span, int totalAppointments, double totalAppointmentsAmount)>> GetTotalAppointmentsTotalAppointmentsAmountAsync(
        DateTime startDate, DateTime endDate, string? timeSpanType);

        Task<(int totalBookings, double totalBookingsAmount)> GetTotalAppointmentsAndAmount();
    }
}
