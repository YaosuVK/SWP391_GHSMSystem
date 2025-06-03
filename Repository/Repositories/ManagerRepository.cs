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
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        private readonly ManagerDAO _managerDao;

        public ManagerRepository(ManagerDAO managerDao) : base(managerDao)
        {
            _managerDao = managerDao;
        }
        public async Task<Manager> AddAsync(Manager entity)
        {
            return await _managerDao.AddAsync(entity);
        }

        public async Task<Manager> UpdateAsync(Manager entity)
        {
            return await _managerDao.UpdateAsync(entity);
        }
    }
}
