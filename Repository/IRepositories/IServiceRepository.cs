using BusinessObject.Model;
using Google.Api;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository.IRepositories
{
    public interface IServiceRepository : IBaseRepository<Services>
    {
        Task<IEnumerable<Services>> GetAllServiceAsync();
        Task<IEnumerable<Services>> GetServicesByIdsAsync(List<int> servicesIds);
    }
}
