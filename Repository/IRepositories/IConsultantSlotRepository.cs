using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IConsultantSlotRepository : IBaseRepository<ConsultantSlot>
    {
        Task<ConsultantSlot> AddSlotAsync(ConsultantSlot cs);
        Task<ConsultantSlot> GetByIdAsync(string consultantId, int slotId);
        Task<ConsultantSlot> GetByConsultantAndSlotAsync(string consultantId, int slotId);
        Task DeleteAsync(ConsultantSlot cs);
        Task<IEnumerable<ConsultantSlot>> GetByConsultantAsync(string consId);
        Task<IEnumerable<ConsultantSlot>> GetAllConsultantSlot();
    }
}
