using AutoMapper;
using BusinessObject.Model;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Services;
using Service.RequestAndResponse.Request.Slot;
using Service.RequestAndResponse.Response.Services;
using Service.RequestAndResponse.Response.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ServicesService : IServiceService
    {
        private readonly IMapper _mapper;
        private readonly IClinicRepository _clinicRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IImageServiceRepository _imageServiceRepository;
        private readonly Cloudinary _cloudinary;
        private readonly IAppointmentDetailRepository _appointmentDetailRepository;

        public ServicesService(IMapper mapper, IClinicRepository clinicRepository,
                ICategoryRepository categoryRepository, IServiceRepository serviceRepository,
                Cloudinary cloudinary, IImageServiceRepository imageServiceRepository,
                IAppointmentDetailRepository appointmentDetailRepository)
        {
            _mapper = mapper;
            _clinicRepository = clinicRepository;
            _categoryRepository = categoryRepository;
            _serviceRepository = serviceRepository;
            _cloudinary = cloudinary;
            _imageServiceRepository = imageServiceRepository;
            _appointmentDetailRepository = appointmentDetailRepository;
        }

        public async Task<BaseResponse<Services>> AddAsync(CreateServiceRequest entity)
        {
            var category = await _categoryRepository.GetByIdAsync(entity.CategoryID);
            if (category == null)
            {
                return new BaseResponse<Services>("Cannot find the clinic", StatusCodeEnum.NotFound_404, null);
            }

            var clinic = await _clinicRepository.GetClinicById(entity.ClinicID);
            if (clinic == null)
            {
                return new BaseResponse<Services>("Cannot find the category", StatusCodeEnum.NotFound_404, null);
            }

            if (entity.ServiceType == ServiceType.TestingSTI && (!entity.ServicesPrice.HasValue || entity.ServicesPrice <= 0))
            {
                return new BaseResponse<Services>("ServicesPrice is required and must be greater than 0 for STI Testing services.", StatusCodeEnum.BadRequest_400, null);
            }

            if (entity.ServiceType == ServiceType.Consultation && entity.ServicesPrice.HasValue)
            {
                return new BaseResponse<Services>("ServicesPrice should not be provided for Consultation services.", StatusCodeEnum.BadRequest_400, null);
            }

            Services service = _mapper.Map<Services>(entity);
            service.CreateAt = DateTime.Now;
            service.UpdateAt = DateTime.Now;
            service.ServicesPrice = entity.ServiceType == ServiceType.TestingSTI ? entity.ServicesPrice.Value : 0;
            await _serviceRepository.AddAsync(service);

            if (entity.Images != null && entity.Images.Any())
            {
                var imageUrls = await UploadImagesToCloudinary(entity.Images);
                foreach (var url in imageUrls)
                {
                    var imageService = new ImageService
                    {
                        Image = url,
                        ServicesID = service.ServicesID
                    };
                    await _imageServiceRepository.AddImageAsync(imageService);
                }
            }

            return new BaseResponse<Services>("Create Service as base success", StatusCodeEnum.Created_201, service);
        }

        public async Task<BaseResponse<List<GetServiceStats>>> GetServiceUsageStats()
        {
            var rawStats = await _appointmentDetailRepository.GetServiceUsageStatsAsync();
            var stats = rawStats.Select(tuple => new GetServiceStats
            {
                serviceName = tuple.serviceName,
                Count = tuple.count,

            }).ToList();
            if (stats == null || !stats.Any())
            {
                return new BaseResponse<List<GetServiceStats>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<List<GetServiceStats>>("Get all bookings as base success",
                StatusCodeEnum.OK_200, stats);
        }

        public async Task<BaseResponse<IEnumerable<ServicesResponse>>> GetAllAsync()
        {
            IEnumerable<Services> service = await _serviceRepository.GetAllAsync();
            if (service == null || !service.Any())
            {
                return new BaseResponse<IEnumerable<ServicesResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var services = _mapper.Map<IEnumerable<ServicesResponse>>(service);
            if (services == null || !services.Any())
            {
                return new BaseResponse<IEnumerable<ServicesResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<ServicesResponse>>("Get all transactions as base success",
                StatusCodeEnum.OK_200, services);
        }

        public async Task<BaseResponse<Services>> UpdateAsync(int serviceID, UpdateServiceRequest entity)
        {
            var existService = await _serviceRepository.GetByIdAsync(serviceID);
            if (existService == null)
            {
                return new BaseResponse<Services>("Cannot find the service", StatusCodeEnum.NotFound_404, null);
            }

            if (entity.ServiceType == ServiceType.TestingSTI && (!entity.ServicesPrice.HasValue || entity.ServicesPrice <= 0))
            {
                return new BaseResponse<Services>("ServicesPrice is required and must be greater than 0 for STI Testing services.", StatusCodeEnum.BadRequest_400, null);
            }

            if (entity.ServiceType == ServiceType.Consultation && entity.ServicesPrice.HasValue)
            {
                return new BaseResponse<Services>("ServicesPrice should not be provided for Consultation services.", StatusCodeEnum.BadRequest_400, null);
            }

            _mapper.Map(entity, existService);
            existService.UpdateAt = DateTime.UtcNow;
            existService.ServicesPrice = entity.ServiceType == ServiceType.TestingSTI ? entity.ServicesPrice.Value : 0;

            // 3. Cập nhật
            await _serviceRepository.UpdateAsync(existService);
            return new BaseResponse<Services>("Cập nhật dịch vụ thành công.", StatusCodeEnum.OK_200, existService);
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
