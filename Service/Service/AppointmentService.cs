using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Response.Appointments;
using Service.RequestAndResponse.Response.Categories;
using Service.RequestAndResponse.Response.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
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
    }
}
