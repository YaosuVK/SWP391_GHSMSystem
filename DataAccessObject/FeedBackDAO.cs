using BusinessObject.Model;
using BusinessObject.PaginatedLists;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class FeedBackDAO : BaseDAO<FeedBack>
    {
        private readonly GHSMContext _context;

        public FeedBackDAO()
        {
            _context = new GHSMContext();
        }

        public async Task<List<FeedBack>> GetAllFeedBacksAsync()
        {
            return await _context.FeedBacks
                .Include(f => f.Customer)
                .Include(f => f.Appointment)
                .ToListAsync();
        }

        public async Task<FeedBack?> GetFeedBackByIdAsync(int feedBackId)
        {
            return await _context.FeedBacks
                .Include(f => f.Customer)
                .Include(f => f.Appointment)
                .FirstOrDefaultAsync(f => f.FeedBackID == feedBackId);
        }

        public async Task<List<FeedBack>> GetFeedBacksByCustomerIdAsync(string customerId)
        {
            return await _context.FeedBacks
                .Include(f => f.Customer)
                .Include(f => f.Appointment)
                .Where(f => f.CustomerID == customerId)
                .ToListAsync();
        }

        public async Task<List<FeedBack>> GetFeedBacksByAppointmentIdAsync(int appointmentId)
        {
            return await _context.FeedBacks
                .Include(f => f.Customer)
                .Include(f => f.Appointment)
                .Where(f => f.AppointmentID == appointmentId)
                .ToListAsync();
        }

        public async Task<FeedBack?> GetFeedBackByCustomerAndAppointmentAsync(string customerId, int appointmentId)
        {
            return await _context.FeedBacks
                .Include(f => f.Customer)
                .Include(f => f.Appointment)
                .FirstOrDefaultAsync(f => f.CustomerID == customerId && f.AppointmentID == appointmentId);
        }

        public async Task<bool> ExistsFeedBackByCustomerAndAppointmentAsync(string customerId, int appointmentId)
        {
            return await _context.FeedBacks
                .AnyAsync(f => f.CustomerID == customerId && f.AppointmentID == appointmentId);
        }
    }
}
