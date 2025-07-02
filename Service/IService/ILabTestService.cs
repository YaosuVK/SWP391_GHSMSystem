using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.LabTests;
using Service.RequestAndResponse.Response.LabTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ILabTestService
    {
        Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> GetAllLabTestsAsync();
        Task<BaseResponse<GetLabTestByIdResponse>> GetLabTestByIdAsync(int id);
        Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> GetLabTestsByCustomerIdAsync(string customerId);
        Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> GetLabTestsByStaffIdAsync(string staffId);
        Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> GetLabTestsByTreatmentIdAsync(int treatmentId);
        Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> GetLabTestsByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> SearchLabTestsAsync(string? search, int pageIndex, int pageSize);
        Task<BaseResponse<GetLabTestByIdResponse>> CreateLabTestAsync(CreateLabTestRequest request);
        Task<BaseResponse<GetLabTestByIdResponse>> UpdateLabTestAsync(UpdateLabTestRequest request);
        Task<BaseResponse<string>> DeleteLabTestAsync(int id);
    }
} 