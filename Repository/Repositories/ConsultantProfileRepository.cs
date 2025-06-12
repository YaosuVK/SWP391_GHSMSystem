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
    public class ConsultantProfileRepository : BaseRepository<ConsultantProfile>, IConsultantProfileRepository
    {
        private readonly ConsultantProfileDAO _consultantDao;

        public ConsultantProfileRepository(ConsultantProfileDAO consultantDao) : base(consultantDao)
        {
            _consultantDao = consultantDao;
        }

        public async Task<ConsultantProfile?> GetConsultantProfileById(int consultantId)
        {
            return await _consultantDao.GetByIdAsync(consultantId);
        }

        public async Task<ConsultantProfile> AddAsync(ConsultantProfile entity)
        {
            return await _consultantDao.AddAsync(entity);
        }

        public async Task<ConsultantProfile> UpdateAsync(ConsultantProfile entity)
        {
            return await _consultantDao.UpdateAsync(entity);
        }
    }
}
