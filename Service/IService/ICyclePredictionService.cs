using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.CyclePredictions;
using Service.RequestAndResponse.Response.CyclePredictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ICyclePredictionService
    {
        Task<BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>> GetAllCyclePredictionsAsync();
        Task<BaseResponse<GetCyclePredictionByIdResponse>> GetCyclePredictionByIdAsync(int id);
        Task<BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>> GetCyclePredictionsByMenstrualCycleIdAsync(int menstrualCycleId);
        Task<BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>> GetCyclePredictionsByCustomerIdAsync(string customerId);
        Task<BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>> SearchCyclePredictionsAsync(string? search, int pageIndex, int pageSize);
        Task<BaseResponse<IEnumerable<GetAllCyclePredictionResponse>>> GetCyclePredictionsByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<BaseResponse<GetCyclePredictionByIdResponse>> CreateCyclePredictionAsync(CreateCyclePredictionRequest request);
        Task<BaseResponse<GetCyclePredictionByIdResponse>> UpdateCyclePredictionAsync(UpdateCyclePredictionRequest request);
        Task<BaseResponse<string>> DeleteCyclePredictionAsync(int id);
        Task<BaseResponse<GetCyclePredictionByIdResponse>> GenerateCyclePredictionAsync(int menstrualCycleId);
    }
} 