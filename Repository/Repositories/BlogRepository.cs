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
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        private readonly BlogDAO _blogDao;

        public BlogRepository(BlogDAO blogDao) : base(blogDao)
        {
            _blogDao = blogDao;
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            return await _blogDao.GetAllBlogsAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _blogDao.GetByIdAsync(id);
        }

        public Task<Blog> AddAsync(Blog entity)
        {
            return _blogDao.AddAsync(entity);
        }

        public Task<Blog> UpdateAsync(Blog entity)
        {
            return _blogDao.UpdateAsync(entity);
        }

        public Task<Blog> DeleteAsync(Blog entity)
        {
            return _blogDao.DeleteAsync(entity);
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _blogDao.GetBlogByIdAsync(id);
        }
    }
}
