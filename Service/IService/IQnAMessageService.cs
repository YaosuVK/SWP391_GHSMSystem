using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.QnAMessages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IQnAMessageService
    {
        Task<BaseResponse<QnAMessage>> CreateQnAMessageAsync(string userId, int questionId, CreateQnAMessageRequest req);
        Task<BaseResponse<IEnumerable<QnAMessage>>> GetQnAMessagesByQuestionAsync(int questionId);
        Task<BaseResponse<QnAMessage>> GetQnAMessageByIdAsync(int messageId);
    }
} 