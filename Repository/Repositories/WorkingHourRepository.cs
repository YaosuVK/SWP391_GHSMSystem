using BusinessObject.Model;
using DataAccessObject;
using Repository.BaseRepository;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class WorkingHourRepository : BaseRepository<WorkingHour>, IWorkingHourRepository
    {
        private readonly WorkingHourDAO _workingHourDao;
        public WorkingHourRepository(WorkingHourDAO workingHourDao) : base(workingHourDao)
        {
            _workingHourDao = workingHourDao;
        }

        public async Task<IEnumerable<WorkingHour>> GetAllAsync()
        {
            return await _workingHourDao.GetAllAsync();
        }

        public async Task<WorkingHour?> GetWorkingHourById(int workingHourID)
        {
            return await _workingHourDao.GetByIdAsync(workingHourID);
        }

        public async Task<WorkingHour> AddAsync(WorkingHour entity)
        {
            return await _workingHourDao.AddAsync(entity);
        }

        public async Task<WorkingHour> UpdateAsync(WorkingHour entity)
        {
            return await _workingHourDao.UpdateAsync(entity);
        }

        public async Task<WorkingHour> GetByClinicDayAndShiftAsync(int clinicId, DayInWeek dayInWeek)
        {
            return await _workingHourDao.GetByClinicDayAndShiftAsync(clinicId, dayInWeek);
        }
    }
}
