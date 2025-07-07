using AutoMapper;
using BusinessObject.Model;
using CloudinaryDotNet;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AutoCheckoutAppointmentService : IAutoCheckoutAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AutoCheckoutAppointmentService(IMapper mapper, IAppointmentRepository appointmentRepository, 
            ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<BaseResponse<string>> AutoCheckOutAppointments()
        {
            var checkoutAppointments = await _appointmentRepository.GetCheckOutAppointmentsAsync();
            if (checkoutAppointments == null || !checkoutAppointments.Any())
            {
                throw new InvalidOperationException("There are no Appointments for checking-out!");
            }

            foreach (var appointment in checkoutAppointments)
            {
                if (appointment.TreatmentOutcome == null)
                {
                    throw new InvalidOperationException("The appointment does not have a TreatmentOutcome, cannot auto checkout!");
                }

                appointment.Status = AppointmentStatus.Completed;

                var transaction = await _transactionRepository.GetTransactionByAppointmentId(appointment.AppointmentID);
                if (transaction != null && transaction.StatusTransaction == StatusOfTransaction.Pending)
                {
                    transaction.StatusTransaction = StatusOfTransaction.Completed;
                    transaction.FinishDate = DateTime.Now;
                    await _transactionRepository.UpdateAsync(transaction);
                }
                else
                {
                    throw new InvalidOperationException("The booking does not have a transaction!");
                }

                await _appointmentRepository.UpdateAppointmentAsync(appointment);
            }
            return new BaseResponse<string>("The Appointment has been automatically Checkout successfully!", StatusCodeEnum.Created_201, "Success");
        }

        public async Task<BaseResponse<int>> CancelExpiredAppointments()
        {
            var expiredAppointments = await _appointmentRepository.GetExpiredAppointmentsAsync();
            if (expiredAppointments == null || !expiredAppointments.Any())
            {
                throw new InvalidOperationException("There are no Appointments for cancellation!");
            }

            int cancelledCount = 0;

            foreach (var appointment in expiredAppointments)
            {
                appointment.Status = AppointmentStatus.Cancelled;

                if(appointment.paymentStatus != PaymentStatus.FullyPaid)
                {
                    appointment.paymentStatus = PaymentStatus.Pending;
                }

                if (appointment.paymentStatus == PaymentStatus.FullyPaid)
                {
                    var transaction = await _transactionRepository.GetTransactionByAppointmentId(appointment.AppointmentID);
                    if (transaction != null && transaction.StatusTransaction == StatusOfTransaction.Pending)
                    {
                        transaction.StatusTransaction = StatusOfTransaction.Completed;
                        transaction.FinishDate = DateTime.Now;
                        await _transactionRepository.UpdateAsync(transaction);
                    }
                }

                await _appointmentRepository.UpdateAppointmentAsync(appointment);
                cancelledCount++;
            }
            return new BaseResponse<int>("The appointment has been automatically cancelled successfully!", StatusCodeEnum.Created_201, cancelledCount);
        }
    }
}
