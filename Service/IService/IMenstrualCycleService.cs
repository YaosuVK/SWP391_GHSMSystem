using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.MenstrualCycles;
using Service.RequestAndResponse.Response.MenstrualCycles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IMenstrualCycleService
    {
        Task<BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>> GetAllMenstrualCyclesAsync();
        Task<BaseResponse<GetMenstrualCycleByIdResponse>> GetMenstrualCycleByIdAsync(int id);
        Task<BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>> GetMenstrualCyclesByCustomerIdAsync(string customerId);
        Task<BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>> SearchMenstrualCyclesAsync(string? search, int pageIndex, int pageSize);
        Task<BaseResponse<IEnumerable<GetAllMenstrualCycleResponse>>> GetMenstrualCyclesByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<BaseResponse<GetMenstrualCycleByIdResponse>> CreateMenstrualCycleAsync(CreateMenstrualCycleRequest request);
        Task<BaseResponse<GetMenstrualCycleByIdResponse>> UpdateMenstrualCycleAsync(UpdateMenstrualCycleRequest request);
        Task<BaseResponse<string>> DeleteMenstrualCycleAsync(int id);
    }
} 