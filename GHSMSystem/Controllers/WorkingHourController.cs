using BusinessObject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.WorkingHours;
using Service.RequestAndResponse.Response.WorkingHours;

namespace GHSMSystem.Controllers
{
    [Route("api/WorkingHour")]
    [ApiController]
    public class WorkingHourController : ControllerBase
    {
        private readonly IClinicService _clinicService;
        private readonly ISlotService _slotService;
        private readonly IWorkingHourService _workingHourService;
        private readonly IServiceService _serviceService;
        private readonly ICategoryService _categoryService;

        public WorkingHourController(IClinicService clinicService, ISlotService slotService,
                     IWorkingHourService workingHourService, IServiceService serviceService,
                     ICategoryService categoryService)
        {
            _clinicService = clinicService;
            _slotService = slotService;
            _workingHourService = workingHourService;
            _serviceService = serviceService;
            _categoryService = categoryService;
        }

        //[Authorize(Roles = "Consultant, Manager, Staff")]
        [HttpGet]
        [Route("GetWorkingHour")]
        public async Task<ActionResult<BaseResponse<IEnumerable<WorkingHourResponse>>>> GetAllWorkingHour()
        {
            var workinghour = await _workingHourService.GetAllAsync();
            return Ok(workinghour);
        }

        //[Authorize(Roles = "Manager")]
        [HttpPost]
        [Route("CreateWorkingHour")]
        public async Task<ActionResult<BaseResponse<WorkingHour>>> AddWorkingHour(CreateWorkingHourRequest entity)
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

            var workinghour = await _workingHourService.AddAsync(entity);
            return workinghour;
        }

        //[Authorize(Roles = "Manager")]
        [HttpPut]
        [Route("UpdateWorkingHour")]
        public async Task<ActionResult<BaseResponse<WorkingHour>>> UpdateWorkingHour(int workingHourID, UpdateWorkingHourRequest entity)
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
            var workinghour = await _workingHourService.UpdateAsync(workingHourID, entity);
            return workinghour;
        }
    }
}
