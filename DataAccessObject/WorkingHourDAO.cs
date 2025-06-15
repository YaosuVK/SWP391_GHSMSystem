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
    public class WorkingHourDAO : BaseDAO<WorkingHour>
    {
        private readonly GHSMContext _context;

        public WorkingHourDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<WorkingHour> GetByClinicDayAndShiftAsync(int clinicId, DayInWeek dayInWeek)
        {
            var workingHour = await _context.WorkingHours
                .FirstOrDefaultAsync(w =>
                    w.ClinicID == clinicId &&
                    w.DayInWeek == dayInWeek);

            return workingHour;
        }
    }
}
