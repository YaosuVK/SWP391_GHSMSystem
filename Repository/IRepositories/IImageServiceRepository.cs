using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IImageServiceRepository : IBaseRepository<ImageService>
    {
        Task<IEnumerable<ImageService>> GetAllByServiceIdAsync(int serviceId);
        Task<ImageService> GetImageServiceByIdAsync(int id);
        Task<ImageService> AddImageAsync(ImageService image);
    }
}
