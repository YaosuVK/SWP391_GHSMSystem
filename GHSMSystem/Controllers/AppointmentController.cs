using BusinessObject.Model;
using Google.Api;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Appointments;
using Service.RequestAndResponse.Request.Categories;
using Service.RequestAndResponse.Request.Clinic;
using Service.RequestAndResponse.Request.Services;
using Service.RequestAndResponse.Request.Slot;
using Service.RequestAndResponse.Request.VnPayModel;
using Service.RequestAndResponse.Request.WorkingHours;
using Service.RequestAndResponse.Response.Appointments;
using Service.RequestAndResponse.Response.Categories;
using Service.RequestAndResponse.Response.Services;
using Service.RequestAndResponse.Response.Slots;
using Service.RequestAndResponse.Response.WorkingHours;
using Service.Service;
using System.Globalization;

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
        private readonly IAppointmentService _appointmentService;
        private readonly IVnPayService _vpnPayService;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;

        public AppointmentController(IClinicService clinicService, ISlotService slotService, 
                     IWorkingHourService workingHourService, IServiceService serviceService,
                     ICategoryService categoryService, IAppointmentService appointmentService, 
                     IVnPayService vpnPayService, IConfiguration configuration, 
                     IAccountService accountService)
        {
            _clinicService = clinicService;
            _slotService = slotService;
            _workingHourService = workingHourService;
            _serviceService = serviceService;
            _categoryService = categoryService;
            _appointmentService = appointmentService;
            _vpnPayService = vpnPayService;
            _configuration = configuration;
            _accountService = accountService;
        }

        [HttpGet]
        [Route("GetAllAppointment")]
        public async Task<ActionResult<BaseResponse<IEnumerable<GetAllAppointment>>>> GetAllAppointment()
        {
            var appointments = await _appointmentService.GetAllAppointment();
            return Ok(appointments);
        }

        [HttpGet]
        [Route("GetAppointmentByID/{appointmentId}")]
        public async Task<ActionResult<BaseResponse<GetAllAppointment?>>> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
            return Ok(appointment);
        }

        [HttpGet]
        [Route("GetAppointmentByCode/{appointmentCode}")]
        public async Task<ActionResult<BaseResponse<GetAllAppointment?>>> GetAppointmentByCodeAsync(string appointmentCode)
        {
            var appointment = await _appointmentService.GetAppointmentByCodeAsync(appointmentCode);
            return Ok(appointment);
        }

        [HttpGet]
        [Route("GetAppointmentByConsultantID/{accountId}")]
        public async Task<ActionResult<BaseResponse<IEnumerable<GetAllAppointment>>>> GetAppointmentsByConsultantId(string accountId)
        {
            var appointment = await _appointmentService.GetAppointmentsByConsultantId(accountId);
            return Ok(appointment);
        }

        [HttpGet]
        [Route("GetAppointmentByCustomerID/{accountId}")]
        public async Task<ActionResult<BaseResponse<IEnumerable<GetAllAppointment>>>> GetAppointmentsByCustomerId(string accountId)
        {
            var appointment = await _appointmentService.GetAppointmentsByCustomerId(accountId);
            return Ok(appointment);
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

        [HttpGet("vnpay-return")]
        public async Task<IActionResult> HandleVnPayReturn([FromQuery] VnPayReturnModel model)
        {
            if (model.Vnp_TransactionStatus != "00") return BadRequest();
            var (appointmentId, accountId) = _appointmentService.ParseOrderInfo(model.Vnp_OrderInfo);

            var transaction = new Transaction
            {
                Amount = model.Vnp_Amount / 100,
                BankCode = model.Vnp_BankCode,
                BankTranNo = model.Vnp_BankTranNo,
                TransactionType = model.Vnp_CardType,
                OrderInfo = model.Vnp_OrderInfo,
                PayDate = DateTime.ParseExact((string)model.Vnp_PayDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                ResponseCode = model.Vnp_ResponseCode,
                TmnCode = model.Vnp_TmnCode,
                TransactionNo = model.Vnp_TransactionNo,
                TransactionStatus = model.Vnp_TransactionStatus,
                TxnRef = model.Vnp_TxnRef,
                SecureHash = model.Vnp_SecureHash,
                ResponseId = model.Vnp_TransactionNo,
                Message = model.Vnp_ResponseCode
            };

            if (appointmentId.HasValue)
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId.Value);
                if (appointment == null)
                    return BadRequest($"Appointment with ID {appointmentId} not found.");

                await _appointmentService.CreateAppointmentPayment(appointmentId, transaction);
                return Redirect($"{_configuration["VnPay:UrlReturnPayment"]}/{appointmentId}");
            }
            return BadRequest("Cannot find Appointment");
        }

        [HttpGet("vnpay-return-refunded")]
        public async Task<IActionResult> HandleVnPayReturnRefund([FromQuery] VnPayReturnModel model)
        {
            if (model.Vnp_TransactionStatus != "00") return BadRequest();
            var (appointmentId, accountId) = _appointmentService.ParseOrderInfo(model.Vnp_OrderInfo);

            var account = await _accountService.GetByStringId(accountId);

            var transaction = new Transaction
            {
                Account = account,
                Amount = model.Vnp_Amount / 100,
                BankCode = model.Vnp_BankCode,
                BankTranNo = model.Vnp_BankTranNo,
                TransactionType = model.Vnp_CardType,
                OrderInfo = model.Vnp_OrderInfo,
                PayDate = DateTime.ParseExact((string)model.Vnp_PayDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                ResponseCode = model.Vnp_ResponseCode,
                TmnCode = model.Vnp_TmnCode,
                TransactionNo = model.Vnp_TransactionNo,
                TransactionStatus = model.Vnp_TransactionStatus,
                TxnRef = model.Vnp_TxnRef,
                SecureHash = model.Vnp_SecureHash,
                ResponseId = model.Vnp_TransactionNo,
                Message = model.Vnp_ResponseCode
            };

            if (appointmentId.HasValue)
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId.Value);
                if (appointment == null)
                    return BadRequest($"Appointment with ID {appointmentId} not found.");

                await _appointmentService.CreateAppointmentRefundPayment(appointmentId, transaction);
                return Redirect($"{_configuration["VnPay:UrlReturnPayment"]}/{appointmentId}");
            }

            return BadRequest("Cannot find Appointment");
        }

        [HttpPost]
        [Route("CreateAppointment")]
        public async Task<ActionResult<BaseResponse<int>>> CreateAppointment(CreateAppointmentRequest request)
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

            var appointment = await _appointmentService.CreateAppointment(request);
            return appointment;
        }

        [HttpPost]
        [Route("AppointmentPayment")]
        public async Task<ActionResult<string>> CheckOutAppointment(int appointmentID)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentID);
            if (appointment.Data.Status == AppointmentStatus.Completed)
            {
                return BadRequest("This appointment already completed, cannot have a payment");
            }

            if (appointment.Data.Status == AppointmentStatus.Cancelled)
            {
                return BadRequest("This appointment already cancelled, cannot have a payment");
            }

            if (appointment.Data.Status == AppointmentStatus.InProgress)
            {
                return BadRequest("This appointment already paid, cannot have a payment");
            }

            if (appointment.Data.Status == AppointmentStatus.RequestRefund)
            {
                return BadRequest("This appointment already paid, cannot have a payment");
            }

            double amount = appointment.Data.TotalAmount;
            var vnPayModel = new VnPayRequestModel
            {
                AppointmentID = appointment.Data.AppointmentID,
                AccountID = appointment.Data.CustomerID,
                FullName = appointment.Data.Customer.Name,
                Description = $"{appointment.Data.Customer.Name} {appointment.Data.Customer.Phone}",
                Amount = amount,
                CreatedDate = DateTime.UtcNow
            };
            return _vpnPayService.CreatePaymentUrlWeb(HttpContext, vnPayModel);
        }

        [HttpPost]
        [Route("AppointmentPayment-Refund-Full")]
        public async Task<ActionResult<string>> CheckOutRefundAppointmentFull(int appointmentID, string accountId)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentID);

            if (appointment.Data.Status == AppointmentStatus.RequestCancel) 
            {
                double amount = 0;

                if (appointment.Data.paymentStatus == PaymentStatus.FullyPaid)
                {
                    amount = appointment.Data.TotalAmount;
                }

                var vnPayModel = new VnPayRequestModel
                {
                    AppointmentID = appointment.Data.AppointmentID,
                    AccountID = accountId,
                    FullName = appointment.Data.Customer.Name,
                    Description = $"{appointment.Data.Customer.Name} {appointment.Data.Customer.Phone}",
                    Amount = amount,
                    CreatedDate = DateTime.UtcNow
                };
                return _vpnPayService.CreatePaymentUrlWebRefund(HttpContext, vnPayModel);
            }
            else
            {
                return BadRequest("Cannot Refunded");
            }
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

        [HttpPut]
        [Route("UpdateAppointment")]
        public async Task<ActionResult<BaseResponse<UpdateAppointmentRequest>>> UpdateAppointment(int appointmentID, UpdateAppointmentRequest request)
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

            var appointment = await _appointmentService.UpdateAppointment( appointmentID, request);
            return appointment;
        }

        [HttpPut]
        [Route("UpdateAppointmentWithSTIRequest")]
        public async Task<ActionResult<BaseResponse<UpdateApppointmentRequestSTI>>> UpdateAppointmentForTesting(int appointmentID, UpdateApppointmentRequestSTI request)
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

            var appointment = await _appointmentService.UpdateAppointmentForTesting(appointmentID, request);
            return appointment;
        }

        [HttpPut]
        [Route("ChangeAppointmentSlot")]
        public async Task<ActionResult<BaseResponse<UpdateAppointmentSlot>>> ChangeAppointmentSlot(int appointmentID, UpdateAppointmentSlot request)
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

            var appointment = await _appointmentService.ChangeAppointmentSlot(appointmentID, request);
            return appointment;
        }

        [HttpPut]
        [Route("ChangeAppointmentStatus")]
        public async Task<ActionResult<BaseResponse<Appointment>>> ChangeAppointmentStatus(int appointmentID, AppointmentStatus status, PaymentStatus paymentStatus)
        {
            var appointment = await _appointmentService.ChangeAppointmentStatus(appointmentID, status, paymentStatus);
            return Ok(appointment);
        }

        [HttpPut]
        [Route("RescheduleAppointmentWithEmail")]
        public async Task<ActionResult<BaseResponse<UpdateAppointmentSlot>>> RescheduleAppointmentWithEmail(int appointmentID, UpdateAppointmentSlot request)
        {
            var appointment = await _appointmentService.RescheduleAppointmentWithEmail(appointmentID, request);
            return Ok(appointment);
        }
    }
}
