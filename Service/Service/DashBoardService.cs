using AutoMapper;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Response.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IMapper _mapper;
        private readonly IDashBoardRepository _dashBoardRepository;
        
        public DashBoardService(IMapper mapper, IDashBoardRepository dashBoardRepository)
        {
            _mapper = mapper;
            _dashBoardRepository = dashBoardRepository;
        }

        public async Task<BaseResponse<List<GetCurrentWeekRevenue>>> GetCurrentWeekRevenue()
        {
            var revenueData = await _dashBoardRepository.GetCurrentWeekRevenueAsync();

            // Chuyển đổi dữ liệu từ tuple (string, double) thành đối tượng GetCurrentWeekRevenueForHomeStay
            var result = revenueData.Select(d => new GetCurrentWeekRevenue
            {
                Date = d.date,
                totalAppointmentsAmount = d.totalAppointmentsAmount
            }).ToList();

            return new BaseResponse<List<GetCurrentWeekRevenue>>("Get All Success", StatusCodeEnum.OK_200, result);
        }

        public async Task<BaseResponse<GetTotalAppointmentsAndAmount>> GetTotalAppointmentsAndAmount()
        {
            var total = await _dashBoardRepository.GetTotalAppointmentsAndAmount();
            var response = new GetTotalAppointmentsAndAmount
            {

                totalAppointments = total.totalBookings,
                totalAppointmentsAmount = total.totalBookingsAmount
            };
            if (response == null)
            {
                return new BaseResponse<GetTotalAppointmentsAndAmount>("Get Total Fail", StatusCodeEnum.BadRequest_400, null);
            }
            return new BaseResponse<GetTotalAppointmentsAndAmount>("Get All Success", StatusCodeEnum.OK_200, response);
        }

        public async Task<BaseResponse<List<GetTotalAppointmentsTotalAppointmentsAmount>>> GetTotalAppointmentsTotalAppointmentsAmountAsync
            (DateTime startDate, DateTime endDate, string? timeSpanType)
        {
            if (startDate == default(DateTime).Date || endDate == default(DateTime).Date)
            {
                return new BaseResponse<List<GetTotalAppointmentsTotalAppointmentsAmount>>("Please input time", StatusCodeEnum.NotImplemented_501, null);
            }

            if (startDate >= endDate)
            {
                return new BaseResponse<List<GetTotalAppointmentsTotalAppointmentsAmount>>("Please input endDate > startDate", StatusCodeEnum.NotAcceptable_406, null);
            }

            var total = await _dashBoardRepository.GetTotalAppointmentsTotalAppointmentsAmountAsync(startDate, endDate, timeSpanType);
            var response = total.Select(p => new GetTotalAppointmentsTotalAppointmentsAmount
            {
                span = p.span,
                totalAppointments = p.totalAppointments,
                totalAppointmentsAmount = p.totalAppointmentsAmount
            }).ToList();
            if (response == null || !response.Any())
            {
                return new BaseResponse<List<GetTotalAppointmentsTotalAppointmentsAmount>>("Get Total Fail", StatusCodeEnum.BadRequest_400, null);
            }
            return new BaseResponse<List<GetTotalAppointmentsTotalAppointmentsAmount>>("Get All Success", StatusCodeEnum.OK_200, response);
        }
    }
}
