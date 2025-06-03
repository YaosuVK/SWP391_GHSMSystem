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
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly CustomerDAO _customerDao;

        public CustomerRepository(CustomerDAO customerDao) : base(customerDao)
        {
            _customerDao = customerDao;
        }
        public async Task<Customer> AddAsync(Customer entity)
        {
            return await _customerDao.AddAsync(entity);
        }

        public async Task<Customer> UpdateAsync(Customer entity)
        {
            return await _customerDao.UpdateAsync(entity);
        }
    }
}
