using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Categories;
using Service.RequestAndResponse.Request.ConsultantProfiles;
using Service.RequestAndResponse.Response.ConsultantProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IConsultantProfileServive
    {
        Task<BaseResponse<CreateConsultantProfile>> CreateConsultantProfile(CreateConsultantProfile request);
        Task<BaseResponse<UpdateConsultantProfile>> UpdateConsultantProfile(int consultantProfileID, UpdateConsultantProfile request);
        Task<BaseResponse<GetConsultantProfileResponse?>> GetConsultantProfileByID(int? consultantProfileID);
        Task<BaseResponse<IEnumerable<GetAllConsultantProfile?>>> GetAllConsultantProfile();
        Task<BaseResponse<GetConsultantProfileResponse?>> GetConsultantProfileByAccountID(string accountId);
    }
}
