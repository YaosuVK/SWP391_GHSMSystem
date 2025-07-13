using BusinessObject.Model;
using Repository.IBaseRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IChatRepository : IBaseRepository<Conversation>
    {
        Task<IEnumerable<Message>> GetMessagesByConversationAsync(int conversationId);
        Task MarkAllMessagesAsReadAsync(int conversationId, string readerId);
        Task<Message> AddMessageAsync(Message message);
        Task<Conversation> GetOrCreateConversationAsync(string user1Id, string user2Id);
        Task<IEnumerable<Conversation>> GetConversationsByCustomerIdAsync(string customerId);
        Task<IEnumerable<Conversation>> GetConversationsByUserAsync(string userId);
    }
} 