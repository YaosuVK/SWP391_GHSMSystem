using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.WorkingHours;
using Service.RequestAndResponse.Response.WorkingHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IWorkingHourService
    {
        Task<BaseResponse<IEnumerable<WorkingHourResponse>>> GetAllAsync();
        Task<BaseResponse<WorkingHourResponse?>> GetWorkingHourById(int clinicId);
        Task<BaseResponse<WorkingHour>> AddAsync(CreateWorkingHourRequest entity);
        Task<BaseResponse<WorkingHour>> UpdateAsync(int workingHourID,UpdateWorkingHourRequest entity);
    }
}
