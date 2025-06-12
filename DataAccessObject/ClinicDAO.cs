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
    public class ClinicDAO : BaseDAO<Clinic>
    {
        private readonly GHSMContext _context;

        public ClinicDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Clinic?> GetClinicByIdAsync(int appointmentId)
        {
            return await _context.Clinics
                .FirstOrDefaultAsync(o => o.ClinicID == appointmentId);
        }
    }
}
