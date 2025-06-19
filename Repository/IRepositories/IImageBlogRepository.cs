using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IImageBlogRepository : IBaseRepository<ImageBlog>
    {
        Task<IEnumerable<ImageBlog>> GetAllByBlogIdAsync(int blogId);
        Task<ImageBlog> GetImageBlogByIdAsync(int id);
        Task<ImageBlog> AddImageAsync(ImageBlog image);
    }
}
