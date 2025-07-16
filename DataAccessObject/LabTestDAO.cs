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
    public class LabTestDAO : BaseDAO<LabTest>
    {
        private readonly GHSMContext _context;

        public LabTestDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<LabTest>> GetUnassignedLabTestsByCustomerIdAsync(string customerId)
        {
            return await _context.Labtests
                .Include(c => c.TreatmentOutcome)
                .Include(c => c.Customer)
                .Include(c => c.Staff)
                .Where( c => c.CustomerID == customerId && c.TreatmentID == null)
                .ToListAsync();
        }
    }
}
