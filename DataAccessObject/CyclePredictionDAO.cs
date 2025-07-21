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
    public class CyclePredictionDAO : BaseDAO<CyclePrediction>
    {
        private readonly GHSMContext _context;

        public CyclePredictionDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CyclePrediction>> GetAllCycelPredictionAsync()
        {
            return await _context.CyclePredictions
                .Include(c => c.MenstrualCycle)
                .ThenInclude(c => c.Customer)
            .ToListAsync();
        }
    }
} 