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
    public class SlotRepository : BaseRepository<Slot>, ISlotRepository
    {
        private readonly SlotDAO _slotDao;
        public SlotRepository(SlotDAO slotDao) : base(slotDao)
        {
            _slotDao = slotDao;
        }

        public async Task<List<Slot>> AddListAsync(List<Slot> entity)
        {
            return await _slotDao.AddRange(entity);
        }

        public async Task<Slot> UpdateAsync(Slot entity)
        {
            return await _slotDao.UpdateAsync(entity);
        }

        public async Task<Slot> DeleteAsync(Slot entity)
        {
            return await _slotDao.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Slot>> GetAllAsync()
        {
            return await _slotDao.GetAllAsync();
        }

        public async Task<List<Slot>> GetSlotsByWorkingHourId(int workingHourId)
        {
            return await _slotDao.GetSlotsByWorkingHourId(workingHourId);
        }
    }
}
