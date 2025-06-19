using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Blogs;
using Service.RequestAndResponse.Request.Services;
using Service.RequestAndResponse.Response.Blogs;
using Service.RequestAndResponse.Response.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IBlogService
    {
        Task<BaseResponse<IEnumerable<BlogResponse>>> GetAllAsync();
        Task<BaseResponse<Blog>> AddAsync(CreateBlogRequest entity);
        Task<BaseResponse<Blog>> UpdateAsync(int blogID, UpdateBlogRequest entity);
    }
}
