using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Clinic;
using Service.RequestAndResponse.Response.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IClinicService
    {
        Task<BaseResponse<Clinic?>> GetClinicById(int clinicId);
        Task<BaseResponse<Clinic>> CreateClinic(CreateClinicRequest request);
        Task<BaseResponse<Clinic>> UpdateClinic(int clinicID, UpdateClinicRequest request);
    }
}
