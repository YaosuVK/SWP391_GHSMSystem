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
    public class DashBoardRepository : BaseRepository<Appointment>, IDashBoardRepository
    {
        private readonly DashBoardDAO _dashBoardDAO;
        public DashBoardRepository(DashBoardDAO dashBoardDAO) : base(dashBoardDAO)
        {
            _dashBoardDAO = dashBoardDAO;
        }

        public async Task<List<(string date, double totalAppointmentsAmount)>> GetCurrentWeekRevenueAsync()
        {
            return await _dashBoardDAO.GetCurrentWeekRevenueAsync();
        }

        public async Task<(int totalBookings, double totalBookingsAmount)> GetTotalAppointmentsAndAmount()
        {
           return await _dashBoardDAO.GetTotalAppointmentsAndAmount();
        }

        public async Task<List<(object span, int totalAppointments, double totalAppointmentsAmount)>> GetTotalAppointmentsTotalAppointmentsAmountAsync(DateTime startDate, DateTime endDate, string? timeSpanType)
        {
            return await _dashBoardDAO.GetTotalAppointmentsTotalAppointmentsAmountAsync(startDate, endDate, timeSpanType);
        }
    }
}
