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
    public class SlotDAO : BaseDAO<Slot>
    {
        private readonly GHSMContext _context;

        public SlotDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Slot>> GetSlotsByWorkingHourId(int workingHourId)
        {
            return await _context.Slots
            .Where(s => s.WorkingHourID == workingHourId)
            .ToListAsync();
        }

        public async Task<IEnumerable<Slot>> GetAllSlotByDate(DateTime appointmentDate)
        {
            return await _context.Slots
                .Include(s => s.ConsultantSlots)
                .Include(s => s.WorkingHour)
                .Include(s => s.Appointments)
                .Where(s => s.StartTime.Date == appointmentDate.Date || s.EndTime == appointmentDate.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Slot>> GetAvailableSlotsForConsultantAsync(DateTime appointmentDate, string consultantId)
        {
            return await _context.Slots
                .Include(s => s.ConsultantSlots)
                .Include(s => s.WorkingHour)
                .Include(s => s.Appointments)
                .Where(s => s.StartTime.Date == appointmentDate.Date)
                .Where(s => s.ConsultantSlots.Any(cs =>
                    cs.ConsultantID == consultantId &&
                    _context.Appointments.Count(a =>
                        a.SlotID == s.SlotID &&
                        a.ConsultantID == consultantId) <= cs.MaxAppointment))
                .ToListAsync();
        }

        public async Task<IEnumerable<Slot>> GetAvailableSlotsForTestAsync(DateTime appointmentDate)
        {
            return await _context.Slots
                .Include(s => s.WorkingHour)
                .Include(s => s.Appointments)
                .Where(s => s.StartTime.Date == appointmentDate.Date)
                .Where(s =>
                    s.Appointments.Count(a => a.SlotID == s.SlotID && a.ConsultantID == null) <= s.MaxTestAppointment)
                .ToListAsync();
        }

        public async Task<IEnumerable<Slot>> GetAvailableSlotsForTestCanNullAsync(DateTime? appointmentDate)
        {
            return await _context.Slots
                .Include(s => s.WorkingHour)
                .Include(s => s.Appointments)
                .Where(s => s.StartTime.Date == appointmentDate.Value.Date)
                .Where(s =>
                    s.Appointments.Count(a => a.SlotID == s.SlotID && a.ConsultantID == null) <= s.MaxTestAppointment)
                .ToListAsync();
        }
    }
}
