using AutoMapper;
using BusinessObject.Model;
using DataAccessObject;
using Google.Api;
using Microsoft.AspNetCore.Http.HttpResults;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.AppointmentDetails;
using Service.RequestAndResponse.Request.Appointments;
using Service.RequestAndResponse.Response.Appointments;
using Service.RequestAndResponse.Response.Categories;
using Service.RequestAndResponse.Response.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IClinicRepository _clinicalRepository;
        private readonly IAppointmentDetailRepository _appointmentDetailRepository;
        private readonly IConsultantProfileRepository _consultantProfileRepository;
        private readonly ISlotRepository _slotRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public AppointmentService(IMapper mapper, IAppointmentRepository appointmentRepository,
            IServiceRepository serviceRepository, IClinicRepository clinicalRepository,
            IAppointmentDetailRepository appointmentDetailRepository, 
            IConsultantProfileRepository consultantProfileRepository,
            ISlotRepository slotRepository, ITransactionRepository transactionRepository,
            IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _serviceRepository = serviceRepository;
            _clinicalRepository = clinicalRepository;
            _appointmentDetailRepository = appointmentDetailRepository;
            _consultantProfileRepository = consultantProfileRepository;
            _slotRepository = slotRepository;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<BaseResponse<Appointment>> ChangeAppointmentStatus(int appointmentID, AppointmentStatus status, PaymentStatus paymentStatus)
        {
            var appointmentExist = await _appointmentRepository.GetAppointmentByIdAsync(appointmentID);
            if (appointmentExist == null)
            {
                return new BaseResponse<Appointment>("Cannot find your Appointment!",
                         StatusCodeEnum.NotFound_404, null);
            }

            if (status == AppointmentStatus.Cancelled)
            {
                var now = DateTime.Now;
                var daysBeforeAppointment = (appointmentExist.AppointmentDate.Value - now).TotalDays;
                bool hasRefund = daysBeforeAppointment < 1;
                if (!hasRefund)
                {
                    var transaction = await _transactionRepository.GetTransactionByAppointmentId(appointmentExist.AppointmentID);
                    if (transaction != null)
                    {

                        transaction.StatusTransaction = StatusOfTransaction.Completed;
                        transaction.FinishDate = DateTime.Now;
                        await _transactionRepository.UpdateAsync(transaction);
                    }
                }
            }

            if (status == AppointmentStatus.Completed)
            {
                var transaction = await _transactionRepository.GetTransactionByAppointmentId(appointmentExist.AppointmentID);
                if (transaction != null)
                {

                    transaction.StatusTransaction = StatusOfTransaction.Completed;
                    transaction.FinishDate = DateTime.Now;
                    await _transactionRepository.UpdateAsync(transaction);
                }
            }

            var appointment = await _appointmentRepository.ChangeAppointmentStatus(appointmentID, status, paymentStatus);
            return new BaseResponse<Appointment>("Change status ok", StatusCodeEnum.OK_200, appointment);
        }

        public async Task<BaseResponse<int>> CreateAppointment(CreateAppointmentRequest request)
        {
            var unpaidAppointment = await _appointmentRepository.GetUnpaidAppointmentByID(request.CustomerID);
            if (unpaidAppointment != null)
                return new BaseResponse<int>("There is an unpaid appointment. Please complete it before creating a new one.", StatusCodeEnum.Conflict_409, 0);

            if (request.AppointmentDetails == null || !request.AppointmentDetails.Any())
                return new BaseResponse<int>("AppointmentDetails is required.", StatusCodeEnum.BadRequest_400, 0);

            if (string.IsNullOrEmpty(request.CustomerID))
            {
                return new BaseResponse<int>("CustomerID is required.", StatusCodeEnum.BadRequest_400, 0);
            }

            if(request.AppointmentDate == default)
            {
                return new BaseResponse<int>("Please input the AppointmentDate", StatusCodeEnum.BadRequest_400, 0);
            }

            bool isTestAppointment = request.AppointmentDetails.Any(d => d.ServicesID.HasValue);
            bool isConsultantAppointment = request.AppointmentDetails.Any(d => d.ConsultantProfileID.HasValue);

            // Nếu là lịch xét nghiệm, cấm gửi ConsultantID
            if (isTestAppointment && request.ConsultantID != null)
            {
                return new BaseResponse<int>("Cannot select ConsultantID for test appointment, please check again.", StatusCodeEnum.BadRequest_400, 0);
            }

            // Nếu là tư vấn, bắt buộc phải có ConsultantID
            if (isConsultantAppointment && string.IsNullOrEmpty(request.ConsultantID))
            {
                return new BaseResponse<int>("ConsultantID is required for consultant appointment, please check again.", StatusCodeEnum.BadRequest_400, 0);
            }

            var duplicateService = request.AppointmentDetails
                                         .Where(x => x.ServicesID.HasValue)
                                         .GroupBy(x => x.ServicesID)
                                         .FirstOrDefault(g => g.Count() > 1);
            if (duplicateService != null)
            {
                return new BaseResponse<int>($"Duplicate service detected with ServiceID: {duplicateService.Key}. " +
                    $"Please ensure each service is only added once.", StatusCodeEnum.Conflict_409, 0);
            }

            if (request.ConsultantID != null)
            {
                var checkAvailableSlot = await _slotRepository.GetAvailableSlotsForConsultantAsync(request.AppointmentDate, request.ConsultantID);
                if(checkAvailableSlot == null || !checkAvailableSlot.Any())
                {
                    return new BaseResponse<int>("Cannot find the slot suitalble for that day, please choose another day. ", StatusCodeEnum.BadRequest_400, 0);
                }

                if (!checkAvailableSlot.Any(s => s.SlotID == request.SlotID))
                {
                    return new BaseResponse<int>("Selected SlotID is not available for that consultant on the selected day.", StatusCodeEnum.BadRequest_400, 0);
                }

                if (!checkAvailableSlot.Any(s => s.ConsultantSlots.Any(x => x.ConsultantID == request.ConsultantID)))
                {
                    return new BaseResponse<int>("Selected ConsultantID is not available for that consultant on the selected day.", StatusCodeEnum.BadRequest_400, 0);
                }
            }
            else
            {
                var checkTestAvailableSlot = await _slotRepository.GetAvailableSlotsForTestAsync(request.AppointmentDate);
                if (checkTestAvailableSlot == null || !checkTestAvailableSlot.Any())
                {
                    return new BaseResponse<int>("Cannot find the slot suitalble for that day, please choose another day. ", StatusCodeEnum.BadRequest_400, 0);
                }

                if (!checkTestAvailableSlot.Any(s => s.SlotID == request.SlotID))
                {
                    return new BaseResponse<int>("Selected SlotID is not available for test services on the selected day.", StatusCodeEnum.BadRequest_400, 0);
                }
            }
            
            var appointmentDetail = new List<AppointmentDetail>();
            foreach(var detail  in request.AppointmentDetails)
            {
                if ((detail.ConsultantProfileID.HasValue && detail.ServicesID.HasValue) ||
                (!detail.ConsultantProfileID.HasValue && !detail.ServicesID.HasValue))
                {
                    return new BaseResponse<int>("Please choose either ConsultantProfileID or ServicesID, not both or neither.", StatusCodeEnum.BadRequest_400, 0);
                }

                double servicePrice = 0;
                double consultantPrices = 0;
                if(detail.ConsultantProfileID.HasValue && detail.ConsultantProfileID > 0)
                {
                    var consultantProfile = await _consultantProfileRepository.GetConsultantProfileByID(detail.ConsultantProfileID);
                    if(consultantProfile == null)
                    {
                        return new BaseResponse<int>("Invalid consultantProfile selection, please try again!", StatusCodeEnum.Conflict_409, 0);
                    }
                    if(consultantProfile.AccountID != request.ConsultantID)
                    {
                        return new BaseResponse<int>("The ConsultantProfile does not match with Consultant!", StatusCodeEnum.Conflict_409, 0);
                    }

                    consultantPrices = consultantProfile.ConsultantPrice;
                }

                if (detail.ServicesID.HasValue && detail.ServicesID > 0)
                {
                    var service = await _serviceRepository.GetServiceByID(detail.ServicesID);
                    if (service == null)
                    {
                        return new BaseResponse<int>("Invalid Service selection, please try again!", StatusCodeEnum.Conflict_409, 0);
                    }
                    servicePrice = service.ServicesPrice;
                }

                if(detail.Quantity <= 0)
                {
                    return new BaseResponse<int>("Quantity must be > 0, please check again.", StatusCodeEnum.BadRequest_400, 0);
                }

                double unitPrice = detail.ConsultantProfileID.HasValue ? consultantPrices : servicePrice;

                appointmentDetail.Add( new AppointmentDetail
                {
                    ConsultantProfileID = detail.ConsultantProfileID,
                    ServicesID = detail.ServicesID,
                    Quantity = detail.Quantity, // Always be 1 For FE
                    ServicePrice = unitPrice,
                    TotalPrice = detail.Quantity * unitPrice
                });
            }

            var appointment = new Appointment
            {
                CustomerID = request.CustomerID,
                ConsultantID = isConsultantAppointment ? request.ConsultantID : null,
                ClinicID = request.ClinicID,
                SlotID = request.SlotID,
                AppointmentCode = await GenerateUniqueAppointmentCodeAsync(),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                ExpiredTime = DateTime.Now.AddHours(1),
                AppointmentDate = request.AppointmentDate,
                Status = AppointmentStatus.Pending,
                paymentStatus = PaymentStatus.Pending,
                AppointmentDetails = appointmentDetail
            };

            double totalPriceAppointment = appointmentDetail.Sum(d => d.TotalPrice);
            appointment.TotalAmount = totalPriceAppointment;
            appointment.AppointmentType = isConsultantAppointment ? AppointmentType.Consulting : AppointmentType.Testing;

            await _appointmentRepository.AddAsync(appointment);

            return new BaseResponse<int>("Appointment created successfully!", StatusCodeEnum.Created_201, appointment.AppointmentID);
        }

        public async Task<BaseResponse<IEnumerable<GetAllAppointment>>> GetAllAppointment()
        {
            IEnumerable<Appointment> appointment = await _appointmentRepository.GetAllAppointment();
            if (appointment == null)
            {
                return new BaseResponse<IEnumerable<GetAllAppointment>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var appointments = _mapper.Map<IEnumerable<GetAllAppointment>>(appointment);
            if (appointments == null)
            {
                return new BaseResponse<IEnumerable<GetAllAppointment>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<GetAllAppointment>>("Get all appointments as base success",
                StatusCodeEnum.OK_200, appointments);
        }

        public async Task<BaseResponse<GetAllAppointment?>> GetAppointmentByIdAsync(int appointmentId)
        {
            Appointment? appointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
            var result = _mapper.Map<GetAllAppointment>(appointment);
            if (result == null)
            {
                return new BaseResponse<GetAllAppointment?>("Something Went Wrong!", StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<GetAllAppointment?>("Get Transaction as base success", StatusCodeEnum.OK_200, result);
        }


        public async Task<BaseResponse<IEnumerable<GetAllAppointment>>> GetAppointmentsByConsultantId(string accountId)
        {
            IEnumerable<Appointment> appointment = await _appointmentRepository.GetAppointmentsByConsultantId(accountId);
            if (appointment == null)
            {
                return new BaseResponse<IEnumerable<GetAllAppointment>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var appointments = _mapper.Map<IEnumerable<GetAllAppointment>>(appointment);
            if (appointments == null)
            {
                return new BaseResponse<IEnumerable<GetAllAppointment>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<GetAllAppointment>>("Get all appointments as base success",
                StatusCodeEnum.OK_200, appointments);
        }

        public async Task<BaseResponse<IEnumerable<GetAllAppointment>>> GetAppointmentsByCustomerId(string accountId)
        {
            IEnumerable<Appointment> appointment = await _appointmentRepository.GetAppointmentsByCustomerId(accountId);
            if (appointment == null)
            {
                return new BaseResponse<IEnumerable<GetAllAppointment>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var appointments = _mapper.Map<IEnumerable<GetAllAppointment>>(appointment);
            if (appointments == null)
            {
                return new BaseResponse<IEnumerable<GetAllAppointment>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<GetAllAppointment>>("Get all appointments as base success",
                StatusCodeEnum.OK_200, appointments);
        }

        public async Task<BaseResponse<UpdateAppointmentRequest>> UpdateAppointment(int appointmentID, UpdateAppointmentRequest request)
        {
            var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentID);
            if (existingAppointment == null)
            {
                return new BaseResponse<UpdateAppointmentRequest>("Cannot find your Booking!",
                         StatusCodeEnum.NotFound_404, null);
            }

            bool isTestAppointment = request.AppointmentDetails.Any(d => d.ServicesID.HasValue);
            bool isConsultantAppointment = request.AppointmentDetails.Any(d => d.ConsultantProfileID.HasValue);
            
            if (isTestAppointment && isConsultantAppointment)
            {
                return new BaseResponse<UpdateAppointmentRequest>("Cannot mix ConsultantProfileID and ServicesID in one appointment.", StatusCodeEnum.BadRequest_400, null);
            }

            // Nếu là lịch xét nghiệm, cấm gửi ConsultantID
            if (isTestAppointment && request.ConsultantID != null)
            {
                return new BaseResponse<UpdateAppointmentRequest>("Cannot select ConsultantID for test appointment, please check again.", StatusCodeEnum.BadRequest_400, null);
            }

            // Nếu là tư vấn, bắt buộc phải có ConsultantID
            if (isConsultantAppointment && string.IsNullOrEmpty(request.ConsultantID))
            {
                return new BaseResponse<UpdateAppointmentRequest>("ConsultantID is required for consultant appointment, please check again.", StatusCodeEnum.BadRequest_400, null);
            }

            if (request.ConsultantID != null)
            {
                var checkAvailableSlot = await _slotRepository.GetAvailableSlotsForConsultantAsync(request.AppointmentDate, request.ConsultantID);
                if (checkAvailableSlot == null || !checkAvailableSlot.Any())
                {
                    return new BaseResponse<UpdateAppointmentRequest>("Cannot find the slot suitalble for that day, please choose another day. ", StatusCodeEnum.BadRequest_400, null);
                }

                if (!checkAvailableSlot.Any(s => s.SlotID == request.SlotID))
                {
                    return new BaseResponse<UpdateAppointmentRequest>("Selected SlotID is not available for that consultant on the selected day.", StatusCodeEnum.BadRequest_400, null);
                }

                if (!checkAvailableSlot.Any(s => s.ConsultantSlots.Any(x => x.ConsultantID == request.ConsultantID)))
                {
                    return new BaseResponse<UpdateAppointmentRequest>("Selected ConsultantID is not available for that consultant on the selected day.", StatusCodeEnum.BadRequest_400, null);
                }
            }
            else
            {
                var checkTestAvailableSlot = await _slotRepository.GetAvailableSlotsForTestAsync(request.AppointmentDate);
                if (checkTestAvailableSlot == null || !checkTestAvailableSlot.Any())
                {
                    return new BaseResponse<UpdateAppointmentRequest>("Cannot find the slot suitalble for that day, please choose another day. ", StatusCodeEnum.BadRequest_400, null);
                }

                if (!checkTestAvailableSlot.Any(s => s.SlotID == request.SlotID))
                {
                    return new BaseResponse<UpdateAppointmentRequest>("Selected SlotID is not available for test services on the selected day.", StatusCodeEnum.BadRequest_400, null);
                }
            }

            bool isPaid = existingAppointment.paymentStatus == PaymentStatus.FullyPaid;
            bool isCompleted = existingAppointment.Status == AppointmentStatus.Completed;
            bool isCancelled = existingAppointment.Status == AppointmentStatus.Cancelled;

            if (isCompleted)
            {
                return new BaseResponse<UpdateAppointmentRequest>("This booking is already completed and cannot be modified.",
                           StatusCodeEnum.NotFound_404, null);
            }

            if (isCancelled)
            {
                return new BaseResponse<UpdateAppointmentRequest>("This booking is already cancelled and cannot be modified.",
                           StatusCodeEnum.NotFound_404, null);
            }
            if (isPaid)
            {
                return new BaseResponse<UpdateAppointmentRequest>("This booking has already been deposite and cannot be modified.",
             StatusCodeEnum.BadRequest_400, null);
            }
            else
            {
                var existingDetails = existingAppointment.AppointmentDetails.ToList();
                var updatedDetailIds = request.AppointmentDetails
                                       .Select(d => d.AppointmentDetailID)
                                       .Where(id => id.HasValue)
                                       .Select(id => id.Value)
                                       .ToList();

                var detailsToRemove = await _appointmentDetailRepository.GetAppointmentDetailsToRemoveAsync(appointmentID, updatedDetailIds);

                if (detailsToRemove.Any())
                {
                    await _appointmentDetailRepository.DeleteAppointmentDetailAsync(detailsToRemove);
                }

                var duplicatedServiceIDs = request.AppointmentDetails
                   .Where(d => d.ServicesID.HasValue)
                   .GroupBy(d => d.ServicesID)
                   .Where(g => g.Count() > 1)
                   .Select(g => g.Key)
                   .ToList();

                if (duplicatedServiceIDs.Any())
                {
                    return new BaseResponse<UpdateAppointmentRequest>($"ServiceID(s) duplicated in request: {string.Join(", ", duplicatedServiceIDs)}", StatusCodeEnum.Conflict_409, null);
                }
                /*var duplicateService = request.AppointmentDetails
                                         .Where(x => x.ServicesID.HasValue)
                                         .GroupBy(x => x.ServicesID)
                                         .FirstOrDefault(g => g.Count() > 1);*/
                foreach ( var updateAppointmentDetail in request.AppointmentDetails)
                {
                    if ((updateAppointmentDetail.ConsultantProfileID.HasValue && updateAppointmentDetail.ServicesID.HasValue) ||
                    (!updateAppointmentDetail.ConsultantProfileID.HasValue && !updateAppointmentDetail.ServicesID.HasValue))
                    {
                        return new BaseResponse<UpdateAppointmentRequest>("Please choose either ConsultantProfileID or ServicesID, not both or neither.", StatusCodeEnum.BadRequest_400, null);
                    }
                    double servicePrice = 0;
                    double consultantPrices = 0;
                    if (updateAppointmentDetail.ConsultantProfileID.HasValue && updateAppointmentDetail.ConsultantProfileID > 0)
                    {
                        var consultantProfile = await _consultantProfileRepository.GetConsultantProfileByID(updateAppointmentDetail.ConsultantProfileID);
                        if (consultantProfile == null)
                        {
                            return new BaseResponse<UpdateAppointmentRequest>("Invalid consultantProfile selection, please try again!", StatusCodeEnum.Conflict_409, null);
                        }
                        if (consultantProfile.AccountID != request.ConsultantID)
                        {
                            return new BaseResponse<UpdateAppointmentRequest>("The ConsultantProfile does not match with Consultant!", StatusCodeEnum.Conflict_409, null);
                        }

                        consultantPrices = consultantProfile.ConsultantPrice;
                    }

                    if (updateAppointmentDetail.ServicesID.HasValue && updateAppointmentDetail.ServicesID > 0)
                    {
                        var service = await _serviceRepository.GetServiceByID(updateAppointmentDetail.ServicesID);
                        if (service == null)
                        {
                            return new BaseResponse<UpdateAppointmentRequest>("Invalid Service selection, please try again!", StatusCodeEnum.Conflict_409, null);
                        }
                        servicePrice = service.ServicesPrice;
                    }

                    if (updateAppointmentDetail.Quantity <= 0)
                    {
                        return new BaseResponse<UpdateAppointmentRequest>("Quantity must be > 0, please check again.", StatusCodeEnum.BadRequest_400, null);
                    }

                    double unitPrice = updateAppointmentDetail.ConsultantProfileID.HasValue ? consultantPrices : servicePrice;
                    if (unitPrice <= 0)
                    {
                        return new BaseResponse<UpdateAppointmentRequest>("Invalid price detected, please check service/consultant again.", StatusCodeEnum.Conflict_409, null);
                    }

                    if (updateAppointmentDetail.AppointmentDetailID.HasValue)
                    {
                        var existingDetail = existingAppointment.AppointmentDetails
                            .FirstOrDefault(d => d.AppointmentDetailID == updateAppointmentDetail.AppointmentDetailID.Value);

                        if (existingDetail == null)
                            return new BaseResponse<UpdateAppointmentRequest>("Booking detail not found.", StatusCodeEnum.BadRequest_400, null);

                        existingDetail.ConsultantProfileID = updateAppointmentDetail.ConsultantProfileID;
                        existingDetail.ServicesID = updateAppointmentDetail.ServicesID;
                        existingDetail.Quantity = updateAppointmentDetail.Quantity;
                        existingDetail.ServicePrice = unitPrice;
                        existingDetail.TotalPrice = updateAppointmentDetail.Quantity * unitPrice;
                    }
                    else
                    {
                        existingAppointment.AppointmentDetails.Add(new AppointmentDetail
                        {
                            ConsultantProfileID = updateAppointmentDetail.ConsultantProfileID,
                            ServicesID = updateAppointmentDetail.ServicesID,
                            Quantity = updateAppointmentDetail.Quantity, // Always be 1 For FE
                            ServicePrice = unitPrice,
                            TotalPrice = updateAppointmentDetail.Quantity * unitPrice
                        });
                    }
                }
            }

            double totalPriceExistAppointment = existingAppointment.AppointmentDetails.Sum(d => d.TotalPrice);
            existingAppointment.ConsultantID = request.ConsultantID;
            existingAppointment.SlotID = request.SlotID;
            existingAppointment.UpdateAt = DateTime.Now;
            existingAppointment.AppointmentDate = request.AppointmentDate;
            existingAppointment.TotalAmount = totalPriceExistAppointment;
            existingAppointment.AppointmentType = isConsultantAppointment ? AppointmentType.Consulting : AppointmentType.Testing;

            await _appointmentRepository.UpdateAppointmentAsync(existingAppointment);
            return new BaseResponse<UpdateAppointmentRequest>("Booking updated successfully!", StatusCodeEnum.OK_200, request);
        }

        public async Task<Appointment> CreateAppointmentPayment(int? appointmentID, Transaction transaction)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdCanNullAsync(appointmentID);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }
            appointment.Transactions ??= new List<Transaction>();

            bool alreadyExists = appointment.Transactions.Any(t => t.ResponseId == transaction.ResponseId && transaction.ResponseId != null);

            if (alreadyExists)
            {
                throw new Exception("Duplicate transaction detected.");
            }

            transaction.Account = appointment.Customer;

            appointment.Transactions.Add(transaction);

            double totalAmount = appointment.TotalAmount;  // Thay bằng cách tính tổng số tiền thanh toán của booking
            double amountPaid = appointment.Transactions.Sum(t => t.Amount); // Tính tổng số tiền đã thanh toán từ tất cả các giao dịch

            if (amountPaid >= totalAmount)
            {
                appointment.paymentStatus = PaymentStatus.FullyPaid; // Thanh toán đầy đủ
                transaction.TransactionKind = TransactionKind.FullPayment;
            }

            transaction.StatusTransaction = StatusOfTransaction.Pending;
            appointment.Status = AppointmentStatus.Confirmed;

            await _appointmentRepository.UpdateAppointmentAsync(appointment);
            return appointment;
        }

        public async Task<Appointment> CreateAppointmentRefundPayment(int? appointmentID, Transaction transaction)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdCanNullAsync(appointmentID);
            if (appointment == null)
            {
                throw new Exception("Appointment not found");
            }

            var originalTransaction = appointment.Transactions
                      .FirstOrDefault(t => (t.TransactionKind == TransactionKind.FullPayment || t.TransactionKind == TransactionKind.Deposited)
                      && (t.StatusTransaction == StatusOfTransaction.Pending || t.StatusTransaction == StatusOfTransaction.RequestCancel ||
                      t.StatusTransaction == StatusOfTransaction.RequestRefund));

            if (originalTransaction == null)
                throw new Exception("No pending or request cancel payment transaction found to refund.");

            bool wasRequestCancel = originalTransaction.StatusTransaction == StatusOfTransaction.RequestCancel;

            originalTransaction.StatusTransaction = StatusOfTransaction.Cancelled;
            originalTransaction.FinishDate = DateTime.Now;
            await _transactionRepository.UpdateAsync(originalTransaction);

            appointment.Transactions ??= new List<Transaction>();

            bool alreadyExists = appointment.Transactions.Any(t =>
            t.ResponseId == transaction.ResponseId && transaction.ResponseId != null);

            if (alreadyExists)
            {
                throw new Exception("Duplicate transaction detected.");
            }

            transaction.TransactionKind = TransactionKind.Refund;
            transaction.StatusTransaction = StatusOfTransaction.Refunded;
            transaction.FinishDate = DateTime.Now;

            appointment.Transactions.Add(transaction);
            double amountPaid = transaction.Amount;

            if (wasRequestCancel)
            {
                var recipientEmail = appointment.Customer?.Email;
                if (!string.IsNullOrEmpty(recipientEmail))
                {
                    string subject = "Yêu cầu hủy đặt phòng đã được xử lý";
                    string body = $@"
                    <html>
                    <body>
                        <p>Xin chào {appointment.Customer?.Name ?? "quý khách"},</p>
                        <p>Do sự cố phát sinh từ phía Clinic <strong>{appointment.Clinic.Name}</strong>, 
                        đơn đặt phòng của bạn (mã: <strong>{appointment.AppointmentCode}</strong>) đã được hủy và hoàn tiền.</p>
                        <p>Số tiền đã hoàn: <strong>{transaction.Amount:N0} VND</strong>.</p>
                        <p>Chúng tôi rất lấy làm tiếc về sự việc này, rất mong quý khách thông cảm cho bên Clinic</p>
                        <br>
                        <p>Trân trọng,</p>
                        <p><strong>Gender-HealthCare-Service-System</strong></p>
                    </body>
                    </html>";

                    _accountRepository.SendEmail(recipientEmail, subject, body);
                }
            }

            appointment.paymentStatus = PaymentStatus.Refunded;
            appointment.Status = AppointmentStatus.Cancelled;

            await _appointmentRepository.UpdateAppointmentAsync(appointment);
            return appointment;
        }

        public (int? appointmentId, string? accountId) ParseOrderInfo(string orderInfo)
        {
            int? appointmentId = null;
            string? accountId = null;

            if (string.IsNullOrWhiteSpace(orderInfo))
                return (null, null);

            var parts = orderInfo.Split(',', StringSplitOptions.TrimEntries);

            foreach (var part in parts)
            {
                var keyValue = part.Split(':', StringSplitOptions.TrimEntries);
                if (keyValue.Length == 2)
                {
                    switch (keyValue[0])
                    {
                        case "AppointmentID":
                            if (int.TryParse(keyValue[1], out int bId))
                                appointmentId = bId;
                            break;

                        case "AccountID":
                            accountId = keyValue[1];
                            break;
                    }
                }
            }
            return (appointmentId, accountId);
        }

        public async Task<BaseResponse<UpdateAppointmentSlot>> ChangeAppointmentSlot(int appointmentID, UpdateAppointmentSlot request)
        {
            var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentID);
            if (existingAppointment == null)
            {
                return new BaseResponse<UpdateAppointmentSlot>("Cannot find your Appointment!",
                         StatusCodeEnum.NotFound_404, null);
            }

            if (existingAppointment.paymentStatus != PaymentStatus.FullyPaid)
            {
                return new BaseResponse<UpdateAppointmentSlot>("Cannot change the slot of appointment!",
                         StatusCodeEnum.NotFound_404, null);
            }

            bool isTestAppointment = existingAppointment.AppointmentDetails.Any(d => d.ServicesID.HasValue);
            bool isConsultantAppointment = existingAppointment.AppointmentDetails.Any(d => d.ConsultantProfileID.HasValue);
            
            if (isConsultantAppointment)
            {
                var checkAvailableSlot = await _slotRepository.GetAvailableSlotsForConsultantAsync(request.AppointmentDate, existingAppointment.ConsultantID);
                if (checkAvailableSlot == null || !checkAvailableSlot.Any())
                {
                    return new BaseResponse<UpdateAppointmentSlot>("Cannot find the slot suitalble for that day, please choose another day. ", StatusCodeEnum.BadRequest_400, null);
                }

                if (!checkAvailableSlot.Any(s => s.SlotID == request.SlotID))
                {
                    return new BaseResponse<UpdateAppointmentSlot>("Selected SlotID is not available for that consultant on the selected day.", StatusCodeEnum.BadRequest_400, null);
                }
            }

            if(isTestAppointment)
            {
                var checkTestAvailableSlot = await _slotRepository.GetAvailableSlotsForTestAsync(request.AppointmentDate);
                if (checkTestAvailableSlot == null || !checkTestAvailableSlot.Any())
                {
                    return new BaseResponse<UpdateAppointmentSlot>("Cannot find the slot suitalble for that day, please choose another day. ", StatusCodeEnum.BadRequest_400, null);
                }

                if (!checkTestAvailableSlot.Any(s => s.SlotID == request.SlotID))
                {
                    return new BaseResponse<UpdateAppointmentSlot>("Selected SlotID is not available for test services on the selected day.", StatusCodeEnum.BadRequest_400, null);
                }
            }

            existingAppointment.SlotID = request.SlotID;
            existingAppointment.UpdateAt = DateTime.Now;
            existingAppointment.AppointmentDate = request.AppointmentDate;

            await _appointmentRepository.UpdateAppointmentAsync(existingAppointment);
            return new BaseResponse<UpdateAppointmentSlot>("Appointment updated successfully!", StatusCodeEnum.OK_200, request);
        }

        private string GenerateShortAppointmentCode(int length = 8)
        {
            var guidBytes = Guid.NewGuid().ToByteArray();
            var base64 = Convert.ToBase64String(guidBytes);

            var safeCode = base64
                .Replace("/", "")
                .Replace("+", "")
                .Replace("=", "")
                .ToUpper();

            return safeCode.Substring(0, length);
        }

        private async Task<string> GenerateUniqueAppointmentCodeAsync()
        {
            string code;
            do
            {
                code = GenerateShortAppointmentCode();
            }
            while (await _appointmentRepository.ExistsAppointmentCodeAsync(code));

            return code;
        }

        public async Task<BaseResponse<UpdateAppointmentSlot>> RescheduleAppointmentWithEmail(int appointmentID, UpdateAppointmentSlot request)
        {
            var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentID);
            if (existingAppointment == null)
            {
                return new BaseResponse<UpdateAppointmentSlot>("Cannot find your Appointment!",
                         StatusCodeEnum.NotFound_404, null);
            }

            if (existingAppointment.paymentStatus != PaymentStatus.FullyPaid)
            {
                return new BaseResponse<UpdateAppointmentSlot>("Cannot change the slot of appointment!",
                         StatusCodeEnum.NotFound_404, null);
            }

            bool isTestAppointment = existingAppointment.AppointmentDetails.Any(d => d.ServicesID.HasValue);
            bool isConsultantAppointment = existingAppointment.AppointmentDetails.Any(d => d.ConsultantProfileID.HasValue);

            if (isConsultantAppointment)
            {
                var checkAvailableSlot = await _slotRepository.GetAvailableSlotsForConsultantAsync(request.AppointmentDate, existingAppointment.ConsultantID);
                if (checkAvailableSlot == null || !checkAvailableSlot.Any())
                {
                    return new BaseResponse<UpdateAppointmentSlot>("Cannot find the slot suitalble for that day, please choose another day. ", StatusCodeEnum.BadRequest_400, null);
                }

                if (!checkAvailableSlot.Any(s => s.SlotID == request.SlotID))
                {
                    return new BaseResponse<UpdateAppointmentSlot>("Selected SlotID is not available for that consultant on the selected day.", StatusCodeEnum.BadRequest_400, null);
                }
            }

            if (isTestAppointment)
            {
                var checkTestAvailableSlot = await _slotRepository.GetAvailableSlotsForTestAsync(request.AppointmentDate);
                if (checkTestAvailableSlot == null || !checkTestAvailableSlot.Any())
                {
                    return new BaseResponse<UpdateAppointmentSlot>("Cannot find the slot suitalble for that day, please choose another day. ", StatusCodeEnum.BadRequest_400, null);
                }

                if (!checkTestAvailableSlot.Any(s => s.SlotID == request.SlotID))
                {
                    return new BaseResponse<UpdateAppointmentSlot>("Selected SlotID is not available for test services on the selected day.", StatusCodeEnum.BadRequest_400, null);
                }
            }

            existingAppointment.SlotID = request.SlotID;
            existingAppointment.UpdateAt = DateTime.Now;
            existingAppointment.AppointmentDate = request.AppointmentDate;

            await _appointmentRepository.UpdateAppointmentAsync(existingAppointment);

            var recipientEmail = existingAppointment.Customer?.Email;
            if (!string.IsNullOrEmpty(recipientEmail) && IsValidEmail(recipientEmail))
            {
                string subject = "Thông báo dời lịch hẹn do sự cố từ phía phòng khám";
                string body = $@"
                    <html>
                    <body>
                        <p>Xin chào {existingAppointment.Customer?.Name ?? "quý khách"},</p>
                        <p>Do sự cố phát sinh từ phía Clinic <strong>{existingAppointment.Clinic.Name}</strong>, 
                        đơn đặt phòng của bạn (mã: <strong>{existingAppointment.AppointmentCode}</strong>) đã được dời lịch sang thời gian khác</p>
                        <p>Chúng tôi rất xin lỗi vì sự bất tiện này. Lịch hẹn mới của bạn đã được cập nhật như sau:</p>

                        <ul>
                            <li><strong>Ngày mới:</strong> {request.AppointmentDate:dd/MM/yyyy}</li>
                            <li><strong>Khung giờ mới:</strong> {request.SlotID}</li>
                        </ul>

                        <p>Mọi thắc mắc, quý khách vui lòng liên hệ bộ phận hỗ trợ qua số điện thoại: <strong>{existingAppointment.Clinic.Phone}</strong> để được giải đáp.</p>
                        <br>
                        <p>Chúng tôi rất mong nhận được sự thông cảm từ quý khách.</p>
                        <br>
                        <p>Trân trọng,</p>
                        <p><strong>Gender-HealthCare-Service-System</strong></p>
                    </body>
                    </html>";
                try
                {
                    _accountRepository.SendEmail(recipientEmail, subject, body);
                }
                catch (Exception ex)
                {
                    return new BaseResponse<UpdateAppointmentSlot>($"Gửi email thất bại: {ex.Message}", StatusCodeEnum.BadRequest_400, null);
                }
            }
            else
            {
                return new BaseResponse<UpdateAppointmentSlot>("Send Email Fail!", StatusCodeEnum.BadRequest_400, null);
            }
            return new BaseResponse<UpdateAppointmentSlot>("Appointment updated successfully!", StatusCodeEnum.OK_200, request);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BaseResponse<GetAllAppointment?>> GetAppointmentByCodeAsync(string appointmentCode)
        {
            Appointment? appointment = await _appointmentRepository.GetAppointmentByCodeAsync(appointmentCode);
            var result = _mapper.Map<GetAllAppointment>(appointment);
            if (result == null)
            {
                return new BaseResponse<GetAllAppointment?>("Something Went Wrong!", StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<GetAllAppointment?>("Get Transaction as base success", StatusCodeEnum.OK_200, result);
        }
    }
}
