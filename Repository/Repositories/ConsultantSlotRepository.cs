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
    public class ConsultantSlotRepository : BaseRepository<ConsultantSlot>, IConsultantSlotRepository
    {
        private readonly ConsultantSlotDAO _consultantSlotDao;

        public ConsultantSlotRepository(ConsultantSlotDAO consultantSlotDao) : base(consultantSlotDao)
        {
            _consultantSlotDao = consultantSlotDao;
        }

        public Task<ConsultantSlot> AddSlotAsync(ConsultantSlot cs)
        {
            return _consultantSlotDao.AddSlotAsync(cs);
        }

        public Task<ConsultantSlot> GetByConsultantAndSlotAsync(string consultantId, int slotId)
        {
            return _consultantSlotDao.GetByConsultantAndSlotAsync(consultantId, slotId);
        }

        public Task<IEnumerable<ConsultantSlot>> GetByConsultantAsync(string consId)
        {
            return _consultantSlotDao.GetByConsultantAsync(consId);
        }

        public Task<ConsultantSlot> GetByIdAsync(string consultantId, int slotId)
        {
            return _consultantSlotDao.GetByIdAsync(consultantId, slotId);
        }

        Task IConsultantSlotRepository.DeleteAsync(ConsultantSlot cs)
        {
           return _consultantSlotDao.DeleteAsync(cs);
        }
    }
}
