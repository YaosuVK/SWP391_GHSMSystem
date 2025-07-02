using BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Blogs;
using Service.RequestAndResponse.Response.Blogs;
using Service.Service;

namespace GHSMSystem.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [Route("GetAllBlog")]
        public async Task<ActionResult<BaseResponse<IEnumerable<BlogResponse>>>> GetAllAsync()
        {
            var blogs = await _blogService.GetAllAsync();
            return Ok(blogs);
        }

        [HttpGet]
        [Route("GetBlogByID")]
        public async Task<ActionResult<BaseResponse<BlogResponse?>>> GetBlogById(int blogID)
        {
            var blogs = await _blogService.GetBlogById(blogID);
            return Ok(blogs);
        }

        [HttpPost]
        [Route("CreateBlog")]
        public async Task<ActionResult<BaseResponse<Blog>>> AddAsync(CreateBlogRequest entity)
        {
            if (entity == null)
            {
                return BadRequest("Please Implement all Information");
            }

            if (!ModelState.IsValid)
            {
                // Trả về lỗi chi tiết từ ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { message = "Validation Failed", errors });
            }

            var blog = await _blogService.AddAsync(entity);
            return blog;
        }

        [HttpPut]
        [Route("UpdateBlog")]
        public async Task<ActionResult<BaseResponse<Blog>>> UpdateAsync(int blogID, UpdateBlogRequest entity)
        {
            if (entity == null)
            {
                return BadRequest("Please Implement all Information");
            }

            if (!ModelState.IsValid)
            {
                // Trả về lỗi chi tiết từ ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { message = "Validation Failed", errors });
            }

            var blog = await _blogService.UpdateAsync(blogID,entity);
            return blog;
        }
    }
}
