using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface ICyclePredictionRepository : IBaseRepository<CyclePrediction>
    {
        Task<IEnumerable<CyclePrediction>> GetCyclePredictionsByMenstrualCycleIdAsync(int menstrualCycleId);
        Task<IEnumerable<CyclePrediction>> GetCyclePredictionsByCustomerIdAsync(string customerId);
        Task<IEnumerable<CyclePrediction>> SearchCyclePredictionsAsync(string? search, int pageIndex, int pageSize);
        Task<CyclePrediction> GetCyclePredictionWithDetailsAsync(int id);
        Task<IEnumerable<CyclePrediction>> GetCyclePredictionsByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<CyclePrediction> GetLatestCyclePredictionByCustomerAsync(string customerId);
    }
} 