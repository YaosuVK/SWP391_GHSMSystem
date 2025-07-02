using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.MenstrualCycles;
using Service.RequestAndResponse.Response.MenstrualCycles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class MenstrualCycleService : IMenstrualCycleService
    {
        private readonly IMenstrualCycleRepository _menstrualCycleRepository;
        private readonly ICyclePredictionRepository _cyclePredictionRepository;
        private readonly ICyclePredictionService _cyclePredictionService;
        private readonly IMapper _mapper;

        public MenstrualCycleService(
            IMenstrualCycleRepository menstrualCycleRepository,
            ICyclePredictionRepository cyclePredictionRepository,
            ICyclePredictionService cyclePredictionService,
            IMapper mapper)
        {
            _menstrualCycleRepository = menstrualCycleRepository;
            _cyclePredictionRepository = cyclePredictionRepository;
            _cyclePredictionService = cyclePredictionService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>> GetAllMenstrualCyclesAsync()
        {
            try
            {
                var menstrualCycles = await _menstrualCycleRepository.GetAllAsync();
                var response = _mapper.Map<IEnumerable<GetAllMenstrualCycleResponse>>(menstrualCycles);
                return new BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>("Get all menstrual cycles successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetMenstrualCycleByIdResponse>> GetMenstrualCycleByIdAsync(int id)
        {
            try
            {
                var menstrualCycle = await _menstrualCycleRepository.GetMenstrualCycleWithDetailsAsync(id);
                if (menstrualCycle == null)
                {
                    return new BaseResponse<GetMenstrualCycleByIdResponse>("Menstrual cycle not found", StatusCodeEnum.NotFound_404, null);
                }

                var response = _mapper.Map<GetMenstrualCycleByIdResponse>(menstrualCycle);
                
                // Get associated predictions
                var predictions = await _cyclePredictionRepository.GetCyclePredictionsByMenstrualCycleIdAsync(id);
                // s?a l?i
                
                return new BaseResponse<GetMenstrualCycleByIdResponse>("Get menstrual cycle successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetMenstrualCycleByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>> GetMenstrualCyclesByCustomerIdAsync(string customerId)
        {
            try
            {
                var menstrualCycles = await _menstrualCycleRepository.GetMenstrualCyclesByCustomerIdAsync(customerId);
                var response = _mapper.Map<IEnumerable<GetAllMenstrualCycleResponse>>(menstrualCycles);
                return new BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>("Get menstrual cycles by customer successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>> SearchMenstrualCyclesAsync(string? search, int pageIndex, int pageSize)
        {
            try
            {
                var menstrualCycles = await _menstrualCycleRepository.SearchMenstrualCyclesAsync(search, pageIndex, pageSize);
                var response = _mapper.Map<IEnumerable<GetAllMenstrualCycleResponse>>(menstrualCycles);
                return new BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>("Search menstrual cycles successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>> GetMenstrualCyclesByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var menstrualCycles = await _menstrualCycleRepository.GetMenstrualCyclesByDateRangeAsync(fromDate, toDate);
                var response = _mapper.Map<IEnumerable<GetAllMenstrualCycleResponse>>(menstrualCycles);
                return new BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>("Get menstrual cycles by date range successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetMenstrualCycleByIdResponse>> CreateMenstrualCycleAsync(CreateMenstrualCycleRequest request)
        {
            try
            {
                var menstrualCycle = _mapper.Map<MenstrualCycle>(request);
                var createdMenstrualCycle = await _menstrualCycleRepository.AddAsync(menstrualCycle);
                
                // Auto-generate cycle prediction
                await _cyclePredictionService.GenerateCyclePredictionAsync(createdMenstrualCycle.MenstrualCycleID);
                
                var result = await _menstrualCycleRepository.GetMenstrualCycleWithDetailsAsync(createdMenstrualCycle.MenstrualCycleID);
                var response = _mapper.Map<GetMenstrualCycleByIdResponse>(result);
                
                return new BaseResponse<GetMenstrualCycleByIdResponse>("Create menstrual cycle successfully", StatusCodeEnum.Created_201, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetMenstrualCycleByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetMenstrualCycleByIdResponse>> UpdateMenstrualCycleAsync(UpdateMenstrualCycleRequest request)
        {
            try
            {
                var existingMenstrualCycle = await _menstrualCycleRepository.GetByIdAsync(request.MenstrualCycleID);
                if (existingMenstrualCycle == null)
                {
                    return new BaseResponse<GetMenstrualCycleByIdResponse>("Menstrual cycle not found", StatusCodeEnum.NotFound_404, null);
                }

                _mapper.Map(request, existingMenstrualCycle);
                await _menstrualCycleRepository.UpdateAsync(existingMenstrualCycle);
                
                // Regenerate predictions if cycle data changed
                if (request.StartDate.HasValue || request.CycleLength.HasValue || request.PeriodLength.HasValue)
                {
                    await _cyclePredictionService.GenerateCyclePredictionAsync(request.MenstrualCycleID);
                }
                
                var result = await _menstrualCycleRepository.GetMenstrualCycleWithDetailsAsync(request.MenstrualCycleID);
                var response = _mapper.Map<GetMenstrualCycleByIdResponse>(result);
                
                return new BaseResponse<GetMenstrualCycleByIdResponse>("Update menstrual cycle successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetMenstrualCycleByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<string>> DeleteMenstrualCycleAsync(int id)
        {
            try
            {
                var menstrualCycle = await _menstrualCycleRepository.GetByIdAsync(id);
                if (menstrualCycle == null)
                {
                    return new BaseResponse<string>("Menstrual cycle not found", StatusCodeEnum.NotFound_404, null);
                }

                // Delete associated predictions first
                var predictions = await _cyclePredictionRepository.GetCyclePredictionsByMenstrualCycleIdAsync(id);
                foreach (var prediction in predictions)
                {
                    await _cyclePredictionRepository.DeleteAsync(prediction);
                }

                await _menstrualCycleRepository.DeleteAsync(menstrualCycle);
                return new BaseResponse<string>("Delete menstrual cycle successfully", StatusCodeEnum.OK_200, "Deleted successfully");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }
    }
} 