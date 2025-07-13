using BusinessObject.Model;
using Microsoft.AspNetCore.Identity;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.QnAMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class QnAMessageService : IQnAMessageService
    {
        private readonly IQnAMessageRepository _qnaRepo;
        private readonly IQuestionRepository _qRepo;
        private readonly UserManager<BusinessObject.Model.Account> _userManager;
        public QnAMessageService(IQnAMessageRepository qnaRepo, IQuestionRepository qr, UserManager<BusinessObject.Model.Account> um)
        {
            _qnaRepo = qnaRepo; _qRepo = qr; _userManager = um;
        }

        public async Task<BaseResponse<QnAMessage>> CreateQnAMessageAsync(string userId, int questionId, CreateQnAMessageRequest req)
        {
            var q = await _qRepo.GetByIdAsync(questionId);
            if (q == null) return new BaseResponse<QnAMessage>("Question not found", StatusCodeEnum.NotFound_404, null);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new BaseResponse<QnAMessage>("User not found", StatusCodeEnum.NotFound_404, null);

            // Validate parent message if present
            if (req.ParentMessageId.HasValue)
            {
                var parent = await _qnaRepo.GetByIdAsync(req.ParentMessageId.Value);
                if (parent == null || parent.QuestionID != questionId)
                {
                    return new BaseResponse<QnAMessage>("Parent message not found or mismatched question", StatusCodeEnum.BadRequest_400, null);
                }
            }

            // Xác định role của user
            bool isConsultant = await _userManager.IsInRoleAsync(user, "Consultant");

            var m = new QnAMessage
            {
                QuestionID = questionId,
                CustomerID = isConsultant ? null : userId,
                ConsultantID = isConsultant ? userId : null,
                Content = req.Content,
                CreatedAt = System.DateTime.UtcNow,
                ParentMessageId = req.ParentMessageId
            };
            var added = await _qnaRepo.AddAsync(m);
            return new BaseResponse<QnAMessage>("Message created", StatusCodeEnum.Created_201, added);
        }

        public async Task<BaseResponse<IEnumerable<QnAMessage>>> GetQnAMessagesByQuestionAsync(int questionId)
        {
            var q = await _qRepo.GetByIdAsync(questionId);
            if (q == null) return new BaseResponse<IEnumerable<QnAMessage>>("Question not found", StatusCodeEnum.NotFound_404, null);
            var list = await _qnaRepo.GetByQuestionAsync(questionId);
            return list.Any() ? new BaseResponse<IEnumerable<QnAMessage>>("Fetched", StatusCodeEnum.OK_200, list)
                               : new BaseResponse<IEnumerable<QnAMessage>>("No messages", StatusCodeEnum.NotFound_404, null);
        }

        public async Task<BaseResponse<QnAMessage>> GetQnAMessageByIdAsync(int messageId)
        {
            var m = await _qnaRepo.GetByIdAsync(messageId);
            if (m == null) return new BaseResponse<QnAMessage>("Message not found", StatusCodeEnum.NotFound_404, null);
            return new BaseResponse<QnAMessage>("Fetched", StatusCodeEnum.OK_200, m);
        }
    }
} 