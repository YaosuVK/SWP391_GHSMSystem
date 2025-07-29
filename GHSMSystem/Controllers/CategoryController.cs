using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Categories;
using Service.RequestAndResponse.Response.Categories;

namespace GHSMSystem.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Staff, Manager, Consultant")]
        [HttpGet]
        [Route("GetCategory")]
        public async Task<ActionResult<BaseResponse<IEnumerable<GetAllCategoryResponse>>>> GetAllCategory()
        {
            var category = await _categoryService.GetAllCategoryFromBase();
            return Ok(category);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<ActionResult<BaseResponse<CreateCategoryRequest>>> CreateCategory(CreateCategoryRequest categoryRequest)
        {
            if (categoryRequest == null)
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

            var category = await _categoryService.CreateCategoryFromBase(categoryRequest);
            return category;
        }

        [Authorize(Roles = "Manager")]
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<ActionResult<BaseResponse<UpdateCategoryRequest>>> UpdateCategoryFromBase(int id, UpdateCategoryRequest categoryRequest)
        {
            if (categoryRequest == null)
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
            var category = await _categoryService.UpdateCategoryFromBase(id, categoryRequest);
            return category;
        }
    }
}
