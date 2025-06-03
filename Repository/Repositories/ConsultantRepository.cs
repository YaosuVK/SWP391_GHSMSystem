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
    public class ConsultantRepository : BaseRepository<Consultant>, IConsultantRepository
    {
        private readonly ConsultantDAO _consultantDao;

        public ConsultantRepository(ConsultantDAO consultantDao) : base(consultantDao)
        {
            _consultantDao = consultantDao;
        }

        public async Task<Consultant> AddAsync(Consultant entity)
        {
            return await _consultantDao.AddAsync(entity);
        }

        public async Task<Consultant> UpdateAsync(Consultant entity)
        {
            return await _consultantDao.UpdateAsync(entity);
        }
    }
}
