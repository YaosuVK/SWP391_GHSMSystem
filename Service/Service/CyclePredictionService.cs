using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.CyclePredictions;
using Service.RequestAndResponse.Response.CyclePredictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class CyclePredictionService : ICyclePredictionService
    {
        private readonly ICyclePredictionRepository _cyclePredictionRepository;
        private readonly IMenstrualCycleRepository _menstrualCycleRepository;
        private readonly IMapper _mapper;

        public CyclePredictionService(
            ICyclePredictionRepository cyclePredictionRepository,
            IMenstrualCycleRepository menstrualCycleRepository,
            IMapper mapper)
        {
            _cyclePredictionRepository = cyclePredictionRepository;
            _menstrualCycleRepository = menstrualCycleRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>> GetAllCyclePredictionsAsync()
        {
            try
            {
                var cyclePredictions = await _cyclePredictionRepository.GetAllAsync();
                var response = _mapper.Map<IEnumerable<GetAllCyclePredictionResponse>>(cyclePredictions);
                return new BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>("Get all cycle predictions successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetCyclePredictionByIdResponse>> GetCyclePredictionByIdAsync(int id)
        {
            try
            {
                var cyclePrediction = await _cyclePredictionRepository.GetCyclePredictionWithDetailsAsync(id);
                if (cyclePrediction == null)
                {
                    return new BaseResponse<GetCyclePredictionByIdResponse>("Cycle prediction not found", StatusCodeEnum.NotFound_404, null);
                }

                var response = _mapper.Map<GetCyclePredictionByIdResponse>(cyclePrediction);
                return new BaseResponse<GetCyclePredictionByIdResponse>("Get cycle prediction successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetCyclePredictionByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>> GetCyclePredictionsByMenstrualCycleIdAsync(int menstrualCycleId)
        {
            try
            {
                var cyclePredictions = await _cyclePredictionRepository.GetCyclePredictionsByMenstrualCycleIdAsync(menstrualCycleId);
                var response = _mapper.Map<IEnumerable<GetAllCyclePredictionResponse>>(cyclePredictions);
                return new BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>("Get cycle predictions by menstrual cycle successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>> GetCyclePredictionsByCustomerIdAsync(string customerId)
        {
            try
            {
                var cyclePredictions = await _cyclePredictionRepository.GetCyclePredictionsByCustomerIdAsync(customerId);
                var response = _mapper.Map<IEnumerable<GetAllCyclePredictionResponse>>(cyclePredictions);
                return new BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>("Get cycle predictions by customer successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>> SearchCyclePredictionsAsync(string? search, int pageIndex, int pageSize)
        {
            try
            {
                var cyclePredictions = await _cyclePredictionRepository.SearchCyclePredictionsAsync(search, pageIndex, pageSize);
                var response = _mapper.Map<IEnumerable<GetAllCyclePredictionResponse>>(cyclePredictions);
                return new BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>("Search cycle predictions successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>> GetCyclePredictionsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var cyclePredictions = await _cyclePredictionRepository.GetCyclePredictionsByDateRangeAsync(fromDate, toDate);
                var response = _mapper.Map<IEnumerable<GetAllCyclePredictionResponse>>(cyclePredictions);
                return new BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>("Get cycle predictions by date range successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetCyclePredictionByIdResponse>> CreateCyclePredictionAsync(CreateCyclePredictionRequest request)
        {
            try
            {
                var cyclePrediction = _mapper.Map<CyclePrediction>(request);
                var createdCyclePrediction = await _cyclePredictionRepository.AddAsync(cyclePrediction);
                var result = await _cyclePredictionRepository.GetCyclePredictionWithDetailsAsync(createdCyclePrediction.CyclePredictionID);
                var response = _mapper.Map<GetCyclePredictionByIdResponse>(result);
                return new BaseResponse<GetCyclePredictionByIdResponse>("Create cycle prediction successfully", StatusCodeEnum.Created_201, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetCyclePredictionByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetCyclePredictionByIdResponse>> UpdateCyclePredictionAsync(UpdateCyclePredictionRequest request)
        {
            try
            {
                var existingCyclePrediction = await _cyclePredictionRepository.GetByIdAsync(request.CyclePredictionID);
                if (existingCyclePrediction == null)
                {
                    return new BaseResponse<GetCyclePredictionByIdResponse>("Cycle prediction not found", StatusCodeEnum.NotFound_404, null);
                }

                _mapper.Map(request, existingCyclePrediction);
                await _cyclePredictionRepository.UpdateAsync(existingCyclePrediction);
                var result = await _cyclePredictionRepository.GetCyclePredictionWithDetailsAsync(request.CyclePredictionID);
                var response = _mapper.Map<GetCyclePredictionByIdResponse>(result);
                return new BaseResponse<GetCyclePredictionByIdResponse>("Update cycle prediction successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetCyclePredictionByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<string>> DeleteCyclePredictionAsync(int id)
        {
            try
            {
                var cyclePrediction = await _cyclePredictionRepository.GetByIdAsync(id);
                if (cyclePrediction == null)
                {
                    return new BaseResponse<string>("Cycle prediction not found", StatusCodeEnum.NotFound_404, null);
                }

                await _cyclePredictionRepository.DeleteAsync(cyclePrediction);
                return new BaseResponse<string>("Delete cycle prediction successfully", StatusCodeEnum.OK_200, "Deleted successfully");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetCyclePredictionByIdResponse>> GenerateCyclePredictionAsync(int menstrualCycleId)
        {
            try
            {
                var menstrualCycle = await _menstrualCycleRepository.GetByIdAsync(menstrualCycleId);
                if (menstrualCycle == null)
                {
                    return new BaseResponse<GetCyclePredictionByIdResponse>("Menstrual cycle not found", StatusCodeEnum.NotFound_404, null);
                }

                // Delete existing predictions for this cycle
                var existingPredictions = await _cyclePredictionRepository.GetCyclePredictionsByMenstrualCycleIdAsync(menstrualCycleId);
                foreach (var prediction in existingPredictions)
                {
                    await _cyclePredictionRepository.DeleteAsync(prediction);
                }

                // Calculate prediction based on cycle data
                var cyclePrediction = CalculateCyclePrediction(menstrualCycle);
                
                var createdPrediction = await _cyclePredictionRepository.AddAsync(cyclePrediction);
                var result = await _cyclePredictionRepository.GetCyclePredictionWithDetailsAsync(createdPrediction.CyclePredictionID);
                var response = _mapper.Map<GetCyclePredictionByIdResponse>(result);
                
                return new BaseResponse<GetCyclePredictionByIdResponse>("Generate cycle prediction successfully", StatusCodeEnum.Created_201, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetCyclePredictionByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        private CyclePrediction CalculateCyclePrediction(MenstrualCycle cycle)
        {
            // Standard cycle prediction calculations
            var ovulationDay = cycle.CycleLength - 14; // Ovulation typically occurs 14 days before next period
            var fertileStart = ovulationDay - 5; // Fertile window starts 5 days before ovulation
            var fertileEnd = ovulationDay + 1; // Fertile window ends 1 day after ovulation

            return new CyclePrediction
            {
                MenstrualCycleID = cycle.MenstrualCycleID,
                OvulationDate = cycle.StartDate.AddDays(ovulationDay),
                FertileStartDate = cycle.StartDate.AddDays(fertileStart),
                FertileEndDate = cycle.StartDate.AddDays(fertileEnd),
                NextPeriodStartDate = cycle.StartDate.AddDays(cycle.CycleLength)
            };
        }
    }
} 