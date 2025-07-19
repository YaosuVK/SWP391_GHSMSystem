using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

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
            var slots = await _context.Slots
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

            if (appointmentDate.Date == DateTime.Today)
            {
                var currentTime = DateTime.Now.TimeOfDay;
                slots = slots.Where(s => s.StartTime.TimeOfDay >= currentTime).ToList();
            }

            return slots;   
        }

        public async Task<IEnumerable<Slot>> GetAvailableSlotsForTestAsync(DateTime appointmentDate)
        {
            var slots = await _context.Slots
                .Include(s => s.WorkingHour)
                .Include(s => s.Appointments)
                .Where(s => s.StartTime.Date == appointmentDate.Date)
                .Where(s =>
                    s.Appointments.Count(a => a.SlotID == s.SlotID && a.ConsultantID == null) <= s.MaxTestAppointment)
                .ToListAsync();

            if (appointmentDate.Date == DateTime.Today)
            {
                var currentTime = DateTime.Now.TimeOfDay;
                slots = slots.Where(s => s.StartTime.TimeOfDay >= currentTime).ToList();
            }

            return slots;
        }

        public async Task<IEnumerable<Slot>> GetAvailableSlotsForTestCanNullAsync(DateTime? appointmentDate)
        {
            var slots = await _context.Slots
                .Include(s => s.WorkingHour)
                .Include(s => s.Appointments)
                .Where(s => s.StartTime.Date == appointmentDate.Value.Date)
                .Where(s =>
                    s.Appointments.Count(a => a.SlotID == s.SlotID && a.ConsultantID == null) <= s.MaxTestAppointment)
                .ToListAsync();

            if (appointmentDate.Value.Date == DateTime.Today)
            {
                var currentTime = DateTime.Now.TimeOfDay;
                slots = slots.Where(s => s.StartTime.TimeOfDay >= currentTime).ToList();
            }

            return slots;
        }

        public async Task<IEnumerable<Slot>> SearchSlotsAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _context.Slots
                    .Include(s => s.WorkingHour)
                    .Include(s => s.Appointments)
                    .ToListAsync();
            }

            var lowerCaseKeyword = keyword.ToLower();

            // Attempt to parse the keyword as a DateTime
            DateTime searchDateTime;
            bool isDateTime = DateTime.TryParse(keyword, out searchDateTime);

            return await _context.Slots
                .Include(s => s.WorkingHour)
                .Include(s => s.Appointments)
                .Where(s =>
                    (isDateTime && (s.StartTime.Date == searchDateTime.Date || s.EndTime.Date == searchDateTime.Date ||
                                   s.StartTime.TimeOfDay == searchDateTime.TimeOfDay || s.EndTime.TimeOfDay == searchDateTime.TimeOfDay)) ||
                    s.MaxConsultant.ToString().Contains(lowerCaseKeyword) ||
                    s.MaxTestAppointment.ToString().Contains(lowerCaseKeyword) ||
                    s.StartTime.ToString().ToLower().Contains(lowerCaseKeyword) ||
                    s.EndTime.ToString().ToLower().Contains(lowerCaseKeyword))
                .ToListAsync();
        }

        //test
    }
}
