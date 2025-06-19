using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.WorkingHours;
using Service.RequestAndResponse.Response.Transactions;
using Service.RequestAndResponse.Response.WorkingHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class WorkingHourService : IWorkingHourService
    {
        private readonly IMapper _mapper;
        private readonly IWorkingHourRepository _workingHourRepository;

        public WorkingHourService(IMapper mapper, IWorkingHourRepository workingHourRepository)
        {
            _mapper = mapper;
            _workingHourRepository = workingHourRepository;
        }

        public async Task<BaseResponse<WorkingHour>> AddAsync(CreateWorkingHourRequest entity)
        {
            if(entity == null)
            {
                return new BaseResponse<WorkingHour>("WorkingHour request must not be null", StatusCodeEnum.BadRequest_400, null);
            }

            if (!TimeOnly.TryParseExact(entity.OpeningTime, "HH:mm", out var openingTime))
            {
                return new BaseResponse<WorkingHour>("Giờ bắt đầu không đúng định dạng (HH:mm).", StatusCodeEnum.NotAcceptable_406, null);
            }

            if (!TimeOnly.TryParseExact(entity.ClosingTime, "HH:mm", out var closingTime))
            {
                return new BaseResponse<WorkingHour>("Giờ kết thúc không đúng định dạng (HH:mm).", StatusCodeEnum.NotAcceptable_406, null);
            }

            if (openingTime > TimeOnly.MaxValue || closingTime > TimeOnly.MaxValue)
            {
                return new BaseResponse<WorkingHour>("Giờ làm việc không được vượt quá 23:59.", StatusCodeEnum.NotAcceptable_406, null);
            }

            if (openingTime >= closingTime)
            {
                return new BaseResponse<WorkingHour>("Giờ kết thúc phải sau giờ bắt đầu.", StatusCodeEnum.NotAcceptable_406, null);
            }

            var existWorkingHour = await _workingHourRepository.GetByClinicDayAndShiftAsync(entity.ClinicID, entity.DayInWeek);
            if(existWorkingHour != null)
            {
                return new BaseResponse<WorkingHour>("Cannot Create WorkingHour with the same dayinweek", StatusCodeEnum.Conflict_409, null);
            }

            WorkingHour workinghour = _mapper.Map<WorkingHour>(entity);

            workinghour.OpeningTime = openingTime;
            workinghour.ClosingTime = closingTime;

            await _workingHourRepository.AddAsync(workinghour);

            return new BaseResponse<WorkingHour>("Create workinghour as base success", StatusCodeEnum.Created_201, workinghour);
        }

        public async Task<BaseResponse<IEnumerable<WorkingHourResponse>>> GetAllAsync()
        {
            IEnumerable<WorkingHour> workinghour = await _workingHourRepository.GetAllAsync();
            if (workinghour == null || !workinghour.Any())
            {
                return new BaseResponse<IEnumerable<WorkingHourResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var workinghours = _mapper.Map<IEnumerable<WorkingHourResponse>>(workinghour);
            if (workinghours == null || !workinghours.Any())
            {
                return new BaseResponse<IEnumerable<WorkingHourResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<WorkingHourResponse>>("Get all transactions as base success",
                StatusCodeEnum.OK_200, workinghours);
        }

        public async Task<BaseResponse<WorkingHourResponse?>> GetWorkingHourById(int clinicId)
        {
            WorkingHour? workinghour = await _workingHourRepository.GetWorkingHourById(clinicId);
            var result = _mapper.Map<WorkingHourResponse>(workinghour);
            if (result == null)
            {
                return new BaseResponse<WorkingHourResponse?>("Something Went Wrong!", StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<WorkingHourResponse?>("Get WorkingHour as base success", StatusCodeEnum.OK_200, result);
        }

        public async Task<BaseResponse<WorkingHour>> UpdateAsync(int workingHourID, UpdateWorkingHourRequest entity)
        {
            var workingHourExist = await _workingHourRepository.GetByIdAsync(workingHourID);
            if (workingHourExist == null)
            {
                return new BaseResponse<WorkingHour>($"Không tìm thấy giờ làm việc với ID = {workingHourID}", StatusCodeEnum.NotFound_404, null);
            }

            if (entity == null)
            {
                return new BaseResponse<WorkingHour>("WorkingHour request must not be null", StatusCodeEnum.BadRequest_400, null);
            }

            if (!TimeOnly.TryParseExact(entity.OpeningTime, "HH:mm", out var openingTime))
            {
                return new BaseResponse<WorkingHour>("Giờ bắt đầu không đúng định dạng (HH:mm).", StatusCodeEnum.NotAcceptable_406, null);
            }

            if (!TimeOnly.TryParseExact(entity.ClosingTime, "HH:mm", out var closingTime))
            {
                return new BaseResponse<WorkingHour>("Giờ kết thúc không đúng định dạng (HH:mm).", StatusCodeEnum.NotAcceptable_406, null);
            }

            if (openingTime > TimeOnly.MaxValue || closingTime > TimeOnly.MaxValue)
            {
                return new BaseResponse<WorkingHour>("Giờ làm việc không được vượt quá 23:59.", StatusCodeEnum.NotAcceptable_406, null);
            }

            if (openingTime >= closingTime)
            {
                return new BaseResponse<WorkingHour>("Giờ kết thúc phải sau giờ bắt đầu.", StatusCodeEnum.NotAcceptable_406, null);
            }
            // 2. Map dữ liệu từ request vào entity đã tồn tại
            _mapper.Map(entity, workingHourExist);

            workingHourExist.OpeningTime = openingTime;
            workingHourExist.ClosingTime = closingTime;

            await _workingHourRepository.UpdateAsync(workingHourExist);

            // 4. Trả về kết quả
            return new BaseResponse<WorkingHour>("Cập nhật nhật giờ làm việc thành công.", StatusCodeEnum.OK_200, workingHourExist);
        }
    }
}
