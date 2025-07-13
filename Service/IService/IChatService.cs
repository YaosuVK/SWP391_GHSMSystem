using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Conversation;
using Service.RequestAndResponse.Response.Conversation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service.IService
{
    public interface IChatService
    {
        Task<BaseResponse<IEnumerable<Conversation>>> GetConversationsByUserAsync(string userId);
        Task<IEnumerable<Message>> GetMessagesByConversationAsync(int conversationId);
        Task MarkAllMessagesAsReadAsync(int conversationId, string readerId);
        Task<Message> SendMessageAsync(string senderId, string receiverId, string content, string senderName, string receiverName, List<IFormFile> images);
        Task<Conversation> GetOrCreateConversationAsync(string user1Id, string user2Id);
        Task<IEnumerable<Conversation>> GetConversationsByCustomerIdAsync(string customerId);
        Task<int> GetUnreadMessageCountAsync(int conversationId, string userId);
    }
} 