using BusinessObject.Model;
using Microsoft.AspNetCore.Identity;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repo;
        private readonly IQuestionRepository _qRepo;
        private readonly UserManager<Account> _userManager;
        public MessageService(IMessageRepository repo, IQuestionRepository qr, UserManager<Account> um)
        {
            _repo = repo; _qRepo = qr; _userManager = um;
        }

        public async Task<BaseResponse<Message>> CreateMessageAsync(string userId, int questionId, CreateMessageRequest req)
        {
            var q = await _qRepo.GetByIdAsync(questionId);
            if (q == null) return new BaseResponse<Message>("Question not found", StatusCodeEnum.NotFound_404, null);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new BaseResponse<Message>("User not found", StatusCodeEnum.NotFound_404, null);

            // Xác định role của user
            bool isConsultant = await _userManager.IsInRoleAsync(user, "Consultant");

            var m = new Message
            {
                QuestionID = questionId,
                CustomerID = isConsultant ? null : userId,
                ConsultantID = isConsultant ? userId : null,
                Content = req.Content,
                CreatedAt = System.DateTime.UtcNow,
                ParentMessageId = req.ParentMessageId
            };
            var added = await _repo.AddAsync(m);
            return new BaseResponse<Message>("Message created", StatusCodeEnum.Created_201, added);
        }

        public async Task<BaseResponse<IEnumerable<Message>>> GetMessagesByQuestionAsync(int questionId)
        {
            var q = await _qRepo.GetByIdAsync(questionId);
            if (q == null) return new BaseResponse<IEnumerable<Message>>("Question not found", StatusCodeEnum.NotFound_404, null);
            var list = await _repo.GetByQuestionAsync(questionId);
            return list.Any() ? new BaseResponse<IEnumerable<Message>>("Fetched", StatusCodeEnum.OK_200, list)
                               : new BaseResponse<IEnumerable<Message>>("No messages", StatusCodeEnum.NotFound_404, null);
        }
    }
}
