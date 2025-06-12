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
    public class ClinicRepository : BaseRepository<Clinic>, IClinicRepository
    {
        private readonly ClinicDAO _clinicDao;
        public ClinicRepository(ClinicDAO clinicDao) : base(clinicDao)
        {
            _clinicDao = clinicDao;
        }

        public async Task<Clinic?> GetClinicById(int clinicId)
        {
            return await _clinicDao.GetByIdAsync(clinicId);
        }

        public async Task<Clinic> AddAsync(Clinic entity)
        {
            return await _clinicDao.AddAsync(entity);
        }

        public async Task<Clinic> UpdateAsync(Clinic entity)
        {
            return await _clinicDao.UpdateAsync(entity);
        }
    }
}
