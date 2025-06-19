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
    }
}
