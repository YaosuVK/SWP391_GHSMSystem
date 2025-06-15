using BusinessObject.Model;
using DataAccessObject;
using Repository.BaseRepository;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Cloud.Dialogflow.V2.EntityType.Types;

namespace Repository.Repositories
{
    public class ImageServiceRepository : BaseRepository<ImageService>, IImageServiceRepository
    {
        private readonly ImageServicesDAO _imageServicesDao;

        public ImageServiceRepository(ImageServicesDAO imageServicesDao) : base(imageServicesDao)
        {
            _imageServicesDao = imageServicesDao;
        }

        public async Task<ImageService> AddImageAsync(ImageService image)
        {
            return await _imageServicesDao.AddAsync(image);
        }

        public async Task<ImageService> UpdateAsync(ImageService entity)
        {
            return await _imageServicesDao.UpdateAsync(entity);
        }

        public async Task<ImageService> DeleteAsync(ImageService entity)
        {
            return await _imageServicesDao.DeleteAsync(entity);
        }

        public async Task<IEnumerable<ImageService>> GetAllAsync()
        {
            return await _imageServicesDao.GetAllAsync();
        }

        public async Task<IEnumerable<ImageService>> GetAllByServiceIdAsync(int serviceId)
        {
            return await _imageServicesDao.GetAllByServiceIdAsync(serviceId);
        }

        public async Task<ImageService> GetImageServiceByIdAsync(int id)
        {
            return await _imageServicesDao.GetImageServiceByIdAsync(id);
        }

        public async Task<ImageService> AddAsync(ImageService entity)
        {
            return await _imageServicesDao.AddAsync(entity);
        }
    }
}
