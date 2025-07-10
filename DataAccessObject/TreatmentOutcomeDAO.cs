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
    public class TreatmentOutcomeDAO : BaseDAO<TreatmentOutcome>
    {
        private readonly GHSMContext _context;

        public TreatmentOutcomeDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TreatmentOutcome?> GetTreatmenOutComeByAppointmentIdAsync(int appointmentId)
        {
            return await _context.TreatmentOutcomes
                .Include(b => b.Customer)
                .Include(b => b.Consultant)
                .Include(b => b.Appointment)
                .Include(b => b.LabTests)
                .FirstOrDefaultAsync(o => o.AppointmentID == appointmentId);
        }
    }
}
