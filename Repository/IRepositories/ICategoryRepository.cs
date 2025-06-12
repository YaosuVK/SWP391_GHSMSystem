using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> SearchCategoryAsync(string? search, int pageIndex, int pageSize);
        Task<Category> DeleteCategory(int id);
    }
}
