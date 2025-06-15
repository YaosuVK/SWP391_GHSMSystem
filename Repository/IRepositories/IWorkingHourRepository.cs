using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IWorkingHourRepository : IBaseRepository<WorkingHour>
    {
        Task<WorkingHour> GetByClinicDayAndShiftAsync(int clinicId, DayInWeek dayInWeek);
        Task<WorkingHour?> GetWorkingHourById(int workingHourID);
    }
}
