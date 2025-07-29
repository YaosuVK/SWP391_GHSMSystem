using BusinessObject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Services;
using Service.RequestAndResponse.Response.Services;

namespace GHSMSystem.Controllers
{
    [Route("api/Service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {        
        private readonly IServiceService _serviceService;

        public ServiceController( IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [Authorize(Roles = "Staff, Manager, Customer, Consultant")]
        [HttpGet]
        [Route("GetService")]
        public async Task<ActionResult<BaseResponse<IEnumerable<ServicesResponse>>>> GetAllService()
        {
            var service = await _serviceService.GetAllAsync();
            return Ok(service);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        [Route("GetServiceStats")]
        public async Task<ActionResult<BaseResponse<List<GetServiceStats>>>> GetServiceUsageStats()
        {
            var services = await _serviceService.GetServiceUsageStats();
            return Ok(services);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [Route("CreateService")]
        public async Task<ActionResult<BaseResponse<Services>>> AddService(CreateServiceRequest entity)
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

            var service = await _serviceService.AddAsync(entity);
            return service;
        }

        [Authorize(Roles = "Manager")]
        [HttpPut]
        [Route("UpdateService")]
        public async Task<ActionResult<BaseResponse<Services>>> UpdateService(int serviceID, UpdateServiceRequest entity)
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
            var service = await _serviceService.UpdateAsync(serviceID, entity);
            return service;
        }
    }
}
