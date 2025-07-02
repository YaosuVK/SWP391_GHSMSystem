using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IMenstrualCycleRepository : IBaseRepository<MenstrualCycle>
    {
        Task<IEnumerable<MenstrualCycle>> GetMenstrualCyclesByCustomerIdAsync(string customerId);
        Task<IEnumerable<MenstrualCycle>> SearchMenstrualCyclesAsync(string? search, int pageIndex, int pageSize);
        Task<MenstrualCycle> GetMenstrualCycleWithDetailsAsync(int id);
        Task<IEnumerable<MenstrualCycle>> GetMenstrualCyclesByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<MenstrualCycle> GetLatestMenstrualCycleByCustomerAsync(string customerId);
    }
} 