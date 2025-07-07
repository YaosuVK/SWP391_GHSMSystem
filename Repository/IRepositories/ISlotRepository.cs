using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface ISlotRepository : IBaseRepository<Slot>
    {
        Task<List<Slot>> GetSlotsByWorkingHourId(int workingHourId);
        Task<IEnumerable<Slot>> GetAllSlotByDate(DateTime appointmentDate);
        Task<IEnumerable<Slot>> GetAvailableSlotsForConsultantAsync(DateTime appointmentDate, string consultantId);
        Task<IEnumerable<Slot>> GetAvailableSlotsForTestAsync(DateTime appointmentDate);
        Task<IEnumerable<Slot>> GetAvailableSlotsForTestCanNullAsync(DateTime? appointmentDate);
    }
}
