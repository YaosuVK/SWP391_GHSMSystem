using BusinessObject.Model;
using Microsoft.AspNetCore.Identity;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repo;
        private readonly UserManager<Account> _userManager;
        public QuestionService(IQuestionRepository repo, UserManager<Account> um) { _repo = repo; _userManager = um; }

        public async Task<BaseResponse<Question>> CreateQuestionAsync(string customerId, CreateQuestionRequest req)
        {
            var user = await _userManager.FindByIdAsync(customerId);
            if (user == null) return new BaseResponse<Question>("Không tìm thấy khách hàng", StatusCodeEnum.NotFound_404, null);

            var q = new Question { CustomerID = customerId, Content = req.Content, CreatedAt = System.DateTime.UtcNow };
            var added = await _repo.AddAsync(q);
            return new BaseResponse<Question>("Tạo câu hỏi thành công", StatusCodeEnum.Created_201, added);
        }

        public async Task<BaseResponse<IEnumerable<Question>>> GetAllQuestionsAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Any() ? new BaseResponse<IEnumerable<Question>>("Lấy tất cả câu hỏi thành công", StatusCodeEnum.OK_200, list)
                               : new BaseResponse<IEnumerable<Question>>("Không có câu hỏi", StatusCodeEnum.NotFound_404, null);
        }

        public async Task<BaseResponse<Question>> GetQuestionByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            return q != null ? new BaseResponse<Question>("Lấy câu hỏi theo id thành công", StatusCodeEnum.OK_200, q)
                             : new BaseResponse<Question>("Không tìm thấy câu hỏi", StatusCodeEnum.NotFound_404, null);
        }
    }
}
