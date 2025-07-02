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
    public class MenstrualCycleRepository : BaseRepository<MenstrualCycle>, IMenstrualCycleRepository
    {
        private readonly MenstrualCycleDAO _menstrualCycleDAO;

        public MenstrualCycleRepository(MenstrualCycleDAO menstrualCycleDAO) : base(menstrualCycleDAO)
        {
            _menstrualCycleDAO = menstrualCycleDAO;
        }

        public async Task<IEnumerable<MenstrualCycle>> GetMenstrualCyclesByCustomerIdAsync(string customerId)
        {
            return await _menstrualCycleDAO.GetQueryable()
                .Where(mc => mc.CustomerID == customerId)
                .Include(mc => mc.Customer)
                .OrderByDescending(mc => mc.StartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenstrualCycle>> SearchMenstrualCyclesAsync(string? search, int pageIndex, int pageSize)
        {
            var query = _menstrualCycleDAO.GetQueryable()
                .Include(mc => mc.Customer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(mc => 
                    mc.Customer.Name.Contains(search) ||
                    mc.Customer.Email.Contains(search) ||
                    mc.CustomerID.Contains(search));
            }

            return await query
                .OrderByDescending(mc => mc.StartDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<MenstrualCycle> GetMenstrualCycleWithDetailsAsync(int id)
        {
            return await _menstrualCycleDAO.GetQueryable()
                .Where(mc => mc.MenstrualCycleID == id)
                .Include(mc => mc.Customer)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MenstrualCycle>> GetMenstrualCyclesByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _menstrualCycleDAO.GetQueryable()
                .Where(mc => mc.StartDate >= fromDate && mc.StartDate <= toDate)
                .Include(mc => mc.Customer)
                .OrderByDescending(mc => mc.StartDate)
                .ToListAsync();
        }

        public async Task<MenstrualCycle> GetLatestMenstrualCycleByCustomerAsync(string customerId)
        {
            return await _menstrualCycleDAO.GetQueryable()
                .Where(mc => mc.CustomerID == customerId)
                .Include(mc => mc.Customer)
                .OrderByDescending(mc => mc.StartDate)
                .FirstOrDefaultAsync();
        }
    }
} 