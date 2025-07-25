﻿using BusinessObject.Model;
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
    public class ServiceRepository : BaseRepository<Services>, IServiceRepository
    {
        private readonly ServiceDAO _serviceDao;

        public ServiceRepository(ServiceDAO serviceDao) : base(serviceDao)
        {
            _serviceDao = serviceDao;
        }

        public Task<Services> AddAsync(Services entity)
        {
            return _serviceDao.AddAsync(entity);
        }

        public Task<Services> UpdateAsync(Services entity)
        {
            return _serviceDao.UpdateAsync(entity);
        }

        public Task<Services> DeleteAsync(Services entity)
        {
            return _serviceDao.DeleteAsync(entity);
        }

        public Task<IEnumerable<Services>> GetAllAsync()
        {
            return _serviceDao.GetAllAsync();
        }

        public async Task<Services> GetByIdAsync(int id)
        {
            return await _serviceDao.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Services>> GetAllServiceAsync()
        {
            return await _serviceDao.GetAllServiceAsync();
        }

        public async Task<IEnumerable<Services>> GetServicesByIdsAsync(List<int> servicesIds)
        {
            return await _serviceDao.GetServicesByIdsAsync(servicesIds);
        }

        public async Task<Services?> GetServiceByID(int? serviceID)
        {
            return await _serviceDao.GetServiceByID(serviceID);
        }
    }
}
