using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IQuestionService
    {
        Task<BaseResponse<Question>> CreateQuestionAsync(string customerId, CreateQuestionRequest req);
        Task<BaseResponse<IEnumerable<Question>>> GetAllQuestionsAsync();
        Task<BaseResponse<Question>> GetQuestionByIdAsync(int id);
    }
}
