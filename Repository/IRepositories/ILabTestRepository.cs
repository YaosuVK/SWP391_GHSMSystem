using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface ILabTestRepository : IBaseRepository<LabTest>
    {
        Task<IEnumerable<LabTest>> GetLabTestsByCustomerIdAsync(string customerId);
        Task<IEnumerable<LabTest>> GetLabTestsByStaffIdAsync(string staffId);
        Task<IEnumerable<LabTest>> GetLabTestsByTreatmentIdAsync(int treatmentId);
        Task<IEnumerable<LabTest>> SearchLabTestsAsync(string? search, int pageIndex, int pageSize);
        Task<IEnumerable<LabTest>> GetLabTestsByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<LabTest> GetLabTestWithDetailsAsync(int id);
    }
} 