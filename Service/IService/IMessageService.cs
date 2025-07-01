using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IMessageService
    {
        Task<BaseResponse<Message>> CreateMessageAsync(string userId, int questionId, CreateMessageRequest req);
        Task<BaseResponse<IEnumerable<Message>>> GetMessagesByQuestionAsync(int questionId);
    }
}
