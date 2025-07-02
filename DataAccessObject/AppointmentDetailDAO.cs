using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class AppointmentDetailDAO : BaseDAO<AppointmentDetail>
    {
        private readonly GHSMContext _context;

        public AppointmentDetailDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<AppointmentDetail>> GetAppointmentDetailsToRemoveAsync(int bookingId, List<int> updatedDetailIds)
        {
            return await _context.AppointmentDetails
                                 .Where(d => d.AppointmentID == bookingId && !updatedDetailIds.Contains(d.AppointmentDetailID))
                                 .ToListAsync();
        }

        public async Task<List<(string serviceName, int count)>> GetServiceUsageStatsAsync()
        {
            var stats = await _context.AppointmentDetails
                .Include(b => b.Service)
                .Where(b => b.Service != null &&
                (b.Appointment.Status == AppointmentStatus.Confirmed ||
                 b.Appointment.Status == AppointmentStatus.Completed))
                .GroupBy(b => b.Service.ServicesName)
                .Select(g => new
                {
                    serviceName = g.Key,
                    count = g.Count()
                }).ToListAsync();

            return stats.Select(s => (s.serviceName, s.count)).ToList();
        }
    }
}
