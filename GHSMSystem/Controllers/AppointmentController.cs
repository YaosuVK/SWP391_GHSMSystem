using BusinessObject.Model;
using Google.Api;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Categories;
using Service.RequestAndResponse.Request.Clinic;
using Service.RequestAndResponse.Request.Services;
using Service.RequestAndResponse.Request.Slot;
using Service.RequestAndResponse.Request.WorkingHours;
using Service.RequestAndResponse.Response.Categories;
using Service.RequestAndResponse.Response.Services;
using Service.RequestAndResponse.Response.Slots;
using Service.RequestAndResponse.Response.WorkingHours;

namespace GHSMSystem.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IClinicService _clinicService;
        private readonly ISlotService _slotService;
        private readonly IWorkingHourService _workingHourService;
        private readonly IServiceService _serviceService;
        private readonly ICategoryService _categoryService;

        public AppointmentController(IClinicService clinicService, ISlotService slotService, 
                     IWorkingHourService workingHourService, IServiceService serviceService,
                     ICategoryService categoryService)
        {
            _clinicService = clinicService;
            _slotService = slotService;
            _workingHourService = workingHourService;
            _serviceService = serviceService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("GetSlot")]
        public async Task<ActionResult<BaseResponse<IEnumerable<SlotForCustomer>>>> GetAllSlot()
        {
            var slot = await _slotService.GetAllAsync();
            return Ok(slot);
        }

        [HttpGet]
        [Route("GetClinic/{clinicId}")]
        public async Task<ActionResult<BaseResponse<Clinic?>>> GetClinicById(int clinicId)
        {
            var clinic = await _clinicService.GetClinicById(clinicId);
            return Ok(clinic);
        }



        [HttpPost]
        [Route("CreateClinic")]
        public async Task<ActionResult<BaseResponse<Clinic>>> CreateClinic(CreateClinicRequest request)
        {

            if (request == null)
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

            var clinic = await _clinicService.CreateClinic(request);
            return clinic;
        }
/*
        [HttpPost]
        [Route("CreateSlot")]
        public async Task<ActionResult<BaseResponse<List<Slot>>>> AddSlot(CreateSlotRequest entity)
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

            var slot = await _slotService.AddAsync(entity);
            return slot;
        }*/

        [HttpPut]
        [Route("UpdateClinic")]
        public async Task<ActionResult<BaseResponse<Clinic>>> UpdateClinic(int clinicID, UpdateClinicRequest request)
        {
            if (request == null)
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
            var clinic = await _clinicService.UpdateClinic(clinicID, request);
            return clinic;
        }

       /* [HttpPut]
        [Route("UpdateSlot")]
        public async Task<ActionResult<BaseResponse<Slot>>> UpdateAsync(int slotID, UpdateSlotRequest entity)
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
            var slot = await _slotService.UpdateAsync(slotID, entity);
            return slot;
        }*/
    }
}
