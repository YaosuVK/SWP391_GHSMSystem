using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.TreatmentOutcomes;
using Service.RequestAndResponse.Response.TreatmentOutcomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ITreatmentOutcomeService
    {
        Task<BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>> GetAllTreatmentOutcomesAsync();
        Task<BaseResponse<GetTreatmentOutcomeByIdResponse>> GetTreatmentOutcomeByIdAsync(int id);
        Task<BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>> GetTreatmentOutcomesByCustomerIdAsync(string customerId);
        Task<BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>> GetTreatmentOutcomesByConsultantIdAsync(string consultantId);
        Task<BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>> GetTreatmentOutcomesByAppointmentIdAsync(int appointmentId);
        Task<BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>> SearchTreatmentOutcomesAsync(string? search, int pageIndex, int pageSize);
        Task<BaseResponse<GetTreatmentOutcomeByIdResponse>> CreateTreatmentOutcomeAsync(CreateTreatmentOutcomeRequest request);
        Task<BaseResponse<GetTreatmentOutcomeByIdResponse>> UpdateTreatmentOutcomeAsync(UpdateTreatmentOutcomeRequest request);
        Task<BaseResponse<string>> DeleteTreatmentOutcomeAsync(int id);
    }
} 