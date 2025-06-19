using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class ImageBlogDAO : BaseDAO<ImageBlog>
    {
        private readonly GHSMContext _context;

        public ImageBlogDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ImageBlog>> GetAllByBlogIdAsync(int blogId)
        {
            return await _context.ImageBlogs
                        .Where(i => i.BlogID == blogId)
                        .ToListAsync();
        }

        public async Task<ImageBlog> GetImageBlogByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException($"id {id} not found");
            }
            var entity = await _context.ImageBlogs
                                       .SingleOrDefaultAsync(i => i.ImageBlogID == id);
            if (entity == null)
            {
                throw new ArgumentNullException($"Entity with id {id} not found");
            }
            return entity;
        }
    }
}
