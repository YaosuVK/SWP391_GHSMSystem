using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Services;
using Service.RequestAndResponse.Request.Slot;
using Service.RequestAndResponse.Response.Services;
using Service.RequestAndResponse.Response.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IServiceService
    {
        Task<BaseResponse<IEnumerable<ServicesResponse>>> GetAllAsync();
        Task<BaseResponse<Services>> AddAsync(CreateServiceRequest entity);
        Task<BaseResponse<Services>> UpdateAsync(int serviceID, UpdateServiceRequest entity);
    }
}
