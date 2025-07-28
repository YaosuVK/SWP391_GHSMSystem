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
    public class ImageBlogRepository : BaseRepository<ImageBlog>, IImageBlogRepository
    {
        private readonly ImageBlogDAO _imageBlogDao;

        public ImageBlogRepository(ImageBlogDAO imageBlogDao) : base(imageBlogDao)
        {
            _imageBlogDao = imageBlogDao;
        }

        public async Task<ImageBlog> AddImageAsync(ImageBlog image)
        {
            return await _imageBlogDao.AddAsync(image);
        }

        public async Task<IEnumerable<ImageBlog>> GetAllByBlogIdAsync(int blogId)
        {
            return await _imageBlogDao.GetAllByBlogIdAsync(blogId);
        }

        public async Task<ImageBlog> GetImageBlogByIdAsync(int id)
        {
            return await _imageBlogDao.GetImageBlogByIdAsync(id);
        }

        public async Task DeleteImageAsync(int imageId)
        {
            var image = await _imageBlogDao.GetByIdAsync(imageId);
            if (image != null)
            {
                await _imageBlogDao.DeleteAsync(image);
            }
        }
    }
}
