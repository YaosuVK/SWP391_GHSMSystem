using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Response.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ICategoryService
    {
        Task<BaseResponse<IEnumerable<GetAllCategoryResponse>>> GetAllCategoryFromBase();
    }
}
