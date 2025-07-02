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
    public class LabTestRepository : BaseRepository<LabTest>, ILabTestRepository
    {
        private readonly LabTestDAO _labTestDAO;

        public LabTestRepository(LabTestDAO labTestDAO) : base(labTestDAO)
        {
            _labTestDAO = labTestDAO;
        }

        public async Task<IEnumerable<LabTest>> GetLabTestsByCustomerIdAsync(string customerId)
        {
            return await _labTestDAO.GetQueryable()
                .Where(lt => lt.CustomerID == customerId)
                .Include(lt => lt.Customer)
                .Include(lt => lt.Staff)
                .Include(lt => lt.TreatmentOutcome)
                .OrderByDescending(lt => lt.TestDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<LabTest>> GetLabTestsByStaffIdAsync(string staffId)
        {
            return await _labTestDAO.GetQueryable()
                .Where(lt => lt.StaffID == staffId)
                .Include(lt => lt.Customer)
                .Include(lt => lt.Staff)
                .Include(lt => lt.TreatmentOutcome)
                .OrderByDescending(lt => lt.TestDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<LabTest>> GetLabTestsByTreatmentIdAsync(int treatmentId)
        {
            return await _labTestDAO.GetQueryable()
                .Where(lt => lt.TreatmentID == treatmentId)
                .Include(lt => lt.Customer)
                .Include(lt => lt.Staff)
                .Include(lt => lt.TreatmentOutcome)
                .OrderByDescending(lt => lt.TestDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<LabTest>> SearchLabTestsAsync(string? search, int pageIndex, int pageSize)
        {
            var query = _labTestDAO.GetQueryable()
                .Include(lt => lt.Customer)
                .Include(lt => lt.Staff)
                .Include(lt => lt.TreatmentOutcome)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(lt => 
                    lt.TestName.Contains(search) ||
                    lt.Result.Contains(search) ||
                    lt.ReferenceRange.Contains(search) ||
                    lt.Unit.Contains(search) ||
                    lt.Customer.Name.Contains(search) ||
                    lt.Staff.Name.Contains(search));
            }

            return await query
                .OrderByDescending(lt => lt.TestDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<LabTest>> GetLabTestsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _labTestDAO.GetQueryable()
                .Where(lt => lt.TestDate >= fromDate && lt.TestDate <= toDate)
                .Include(lt => lt.Customer)
                .Include(lt => lt.Staff)
                .Include(lt => lt.TreatmentOutcome)
                .OrderByDescending(lt => lt.TestDate)
                .ToListAsync();
        }

        public async Task<LabTest> GetLabTestWithDetailsAsync(int id)
        {
            return await _labTestDAO.GetQueryable()
                .Where(lt => lt.LabTestID == id)
                .Include(lt => lt.Customer)
                .Include(lt => lt.Staff)
                .Include(lt => lt.TreatmentOutcome)
                .FirstOrDefaultAsync();
        }
    }
} 