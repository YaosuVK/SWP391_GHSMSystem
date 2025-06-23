using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.FeedBacks;
using Service.RequestAndResponse.Response.FeedBacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IFeedBackService
    {
        Task<BaseResponse<GetAllFeedBackResponse>> GetAllFeedBacksAsync();
        Task<BaseResponse<GetFeedBackByIdResponse>> GetFeedBackByIdAsync(int feedBackId);
        Task<BaseResponse<GetAllFeedBackResponse>> GetFeedBacksByCustomerIdAsync(string customerId);
        Task<BaseResponse<GetAllFeedBackResponse>> GetFeedBacksByAppointmentIdAsync(int appointmentId);
        Task<BaseResponse<GetFeedBackByIdResponse>> CreateFeedBackAsync(CreateFeedBackRequest request);
        Task<BaseResponse<GetFeedBackByIdResponse>> UpdateFeedBackAsync(int feedBackId, UpdateFeedBackRequest request);
        Task<BaseResponse<object>> DeleteFeedBackAsync(int feedBackId);
    }
} 