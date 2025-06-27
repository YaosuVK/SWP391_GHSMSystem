using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Categories;
using Service.RequestAndResponse.Request.ConsultantProfiles;
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
    }
}
