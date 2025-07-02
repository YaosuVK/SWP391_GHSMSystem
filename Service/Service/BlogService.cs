using AutoMapper;
using BusinessObject.Model;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Blogs;
using Service.RequestAndResponse.Response.Blogs;
using Service.RequestAndResponse.Response.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using Google.Api;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Response.Transactions;

namespace Service.Service
{
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly IImageBlogRepository _imageBlogRepository;
        private readonly Cloudinary _cloudinary;
        public BlogService(IMapper mapper, IBlogRepository blogRepository, Cloudinary cloudinary, IImageBlogRepository imageBlogRepository)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
            _cloudinary = cloudinary;
            _imageBlogRepository = imageBlogRepository;
        }

        public async Task<BaseResponse<Blog>> AddAsync(CreateBlogRequest entity)
        {
            if (entity == null)
            {
                return new BaseResponse<Blog>("Please Input All Information", StatusCodeEnum.BadRequest_400, null);
            }

            Blog blog = _mapper.Map<Blog>(entity);
            blog.CreateAt = DateTime.Now;
            blog.UpdateAt = DateTime.Now;
            await _blogRepository.AddAsync(blog);

            if (entity.Images != null && entity.Images.Any())
            {
                var imageUrls = await UploadImagesToCloudinary(entity.Images);
                foreach (var url in imageUrls)
                {
                    var imageService = new ImageBlog
                    {
                        Image = url,
                        BlogID = blog.BlogID
                    };
                    await _imageBlogRepository.AddImageAsync(imageService);
                }
            }

            return new BaseResponse<Blog>("Create blog as base success", StatusCodeEnum.Created_201, blog);
        }

        public async Task<BaseResponse<IEnumerable<BlogResponse>>> GetAllAsync()
        {
            IEnumerable<Blog> blog = await _blogRepository.GetAllBlogsAsync();
            if (blog == null || !blog.Any())
            {
                return new BaseResponse<IEnumerable<BlogResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var blogs = _mapper.Map<IEnumerable<BlogResponse>>(blog);
            if (blogs == null || !blogs.Any())
            {
                return new BaseResponse<IEnumerable<BlogResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<BlogResponse>>("Get all transactions as base success",
                StatusCodeEnum.OK_200, blogs);
        }

        public async Task<BaseResponse<BlogResponse?>> GetBlogById(int blogID)
        {
            Blog? blog = await _blogRepository.GetBlogByIdAsync(blogID);
            var result = _mapper.Map<BlogResponse>(blog);
            if (result == null)
            {
                return new BaseResponse<BlogResponse?>("Something Went Wrong!", StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<BlogResponse?>("Get Blog as base success", StatusCodeEnum.OK_200, result);
        }

        public async Task<BaseResponse<Blog>> UpdateAsync(int blogID, UpdateBlogRequest entity)
        {
            var existBlog = await _blogRepository.GetByIdAsync(blogID);
            if (existBlog == null)
            {
                return new BaseResponse<Blog>("Cannot find the blog", StatusCodeEnum.NotFound_404, null);
            }

            _mapper.Map(entity, existBlog);
            existBlog.UpdateAt = DateTime.UtcNow;

            // 3. Cập nhật
            await _blogRepository.UpdateAsync(existBlog);
            return new BaseResponse<Blog>("Cập nhật phòng khám thành công.", StatusCodeEnum.OK_200, existBlog);
        }

        private async Task<List<string>> UploadImagesToCloudinary(List<IFormFile> files)
        {
            var urls = new List<string>();

            if (files == null || !files.Any())
            {
                return urls;
            }

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                    continue;

                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "ServiceImages"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.StatusCode == HttpStatusCode.OK)
                {
                    urls.Add(uploadResult.SecureUrl.ToString());
                }
                else
                {
                    throw new Exception($"Failed to upload image to Cloudinary: {uploadResult.Error.Message}");
                }
            }

            return urls;
        }
    }
}
