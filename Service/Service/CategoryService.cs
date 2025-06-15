using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Categories;
using Service.RequestAndResponse.Response.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class CategoryService: ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse<IEnumerable<GetAllCategoryResponse>>> GetAllCategoryFromBase()
        {

            IEnumerable<Category> category = await _categoryRepository.GetAllAsync();
            if (category == null)
            {
                return new BaseResponse<IEnumerable<GetAllCategoryResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var categories = _mapper.Map<IEnumerable<GetAllCategoryResponse>>(category);
            if (categories == null)
            {
                return new BaseResponse<IEnumerable<GetAllCategoryResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<GetAllCategoryResponse>>("Get all category as base success",
                StatusCodeEnum.OK_200, categories);
        }

        public async Task<BaseResponse<CreateCategoryRequest>> CreateCategoryFromBase(CreateCategoryRequest categoryRequest)
        {
            Category category = _mapper.Map<Category>(categoryRequest);
            
            category.CreateAt = DateTime.UtcNow;
            category.UpdateAt = DateTime.UtcNow;
            category.Status = true;

            await _categoryRepository.AddAsync(category);

            var response = _mapper.Map<CreateCategoryRequest>(category);
            return new BaseResponse<CreateCategoryRequest>("Create category as base success", StatusCodeEnum.Created_201, response);
        }

        public async Task<BaseResponse<UpdateCategoryRequest>> UpdateCategoryFromBase(int id, UpdateCategoryRequest categoryRequest)
        {
            Category categoryExist = await _categoryRepository.GetByIdAsync(id);

            _mapper.Map(categoryRequest, categoryExist);

            categoryExist.UpdateAt = DateTime.UtcNow;

            await _categoryRepository.UpdateAsync(categoryExist);

            var result = _mapper.Map<UpdateCategoryRequest>(categoryExist);
            return new BaseResponse<UpdateCategoryRequest>("Update Category as base success", StatusCodeEnum.OK_200, result);
        }
    }
}
