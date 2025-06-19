using BusinessObject.Model;
using DataAccessObject;
using Microsoft.EntityFrameworkCore;
using Repository.BaseRepository;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CyclePredictionRepository : BaseRepository<CyclePrediction>, ICyclePredictionRepository
    {
        private readonly CyclePredictionDAO _cyclePredictionDAO;

        public CyclePredictionRepository(CyclePredictionDAO cyclePredictionDAO) : base(cyclePredictionDAO)
        {
            _cyclePredictionDAO = cyclePredictionDAO;
        }

        public async Task<IEnumerable<CyclePrediction>> GetCyclePredictionsByMenstrualCycleIdAsync(int menstrualCycleId)
        {
            return await _cyclePredictionDAO.GetQueryable()
                .Where(cp => cp.MenstrualCycleID == menstrualCycleId)
                .Include(cp => cp.MenstrualCycle)
                    .ThenInclude(mc => mc.Customer)
                .OrderByDescending(cp => cp.NextPeriodStartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<CyclePrediction>> GetCyclePredictionsByCustomerIdAsync(string customerId)
        {
            return await _cyclePredictionDAO.GetQueryable()
                .Where(cp => cp.MenstrualCycle.CustomerID == customerId)
                .Include(cp => cp.MenstrualCycle)
                    .ThenInclude(mc => mc.Customer)
                .OrderByDescending(cp => cp.NextPeriodStartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<CyclePrediction>> SearchCyclePredictionsAsync(string? search, int pageIndex, int pageSize)
        {
            var query = _cyclePredictionDAO.GetQueryable()
                .Include(cp => cp.MenstrualCycle)
                    .ThenInclude(mc => mc.Customer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(cp => 
                    cp.MenstrualCycle.Customer.FullName.Contains(search) ||
                    cp.MenstrualCycle.Customer.Email.Contains(search) ||
                    cp.MenstrualCycle.CustomerID.Contains(search));
            }

            return await query
                .OrderByDescending(cp => cp.NextPeriodStartDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<CyclePrediction> GetCyclePredictionWithDetailsAsync(int id)
        {
            return await _cyclePredictionDAO.GetQueryable()
                .Where(cp => cp.CyclePredictionID == id)
                .Include(cp => cp.MenstrualCycle)
                    .ThenInclude(mc => mc.Customer)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CyclePrediction>> GetCyclePredictionsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _cyclePredictionDAO.GetQueryable()
                .Where(cp => cp.NextPeriodStartDate >= fromDate && cp.NextPeriodStartDate <= toDate)
                .Include(cp => cp.MenstrualCycle)
                    .ThenInclude(mc => mc.Customer)
                .OrderByDescending(cp => cp.NextPeriodStartDate)
                .ToListAsync();
        }

        public async Task<CyclePrediction> GetLatestCyclePredictionByCustomerAsync(string customerId)
        {
            return await _cyclePredictionDAO.GetQueryable()
                .Where(cp => cp.MenstrualCycle.CustomerID == customerId)
                .Include(cp => cp.MenstrualCycle)
                    .ThenInclude(mc => mc.Customer)
                .OrderByDescending(cp => cp.NextPeriodStartDate)
                .FirstOrDefaultAsync();
        }
    }
} 