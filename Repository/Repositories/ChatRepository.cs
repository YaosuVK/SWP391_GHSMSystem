using BusinessObject.Model;
using DataAccessObject;
using Microsoft.EntityFrameworkCore;
using Repository.BaseRepository;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ChatRepository : BaseRepository<Conversation>, IChatRepository
    {
        private readonly ChatDAO _chatDAO;
        private readonly MessageDAO _messageDAO;

        public ChatRepository(ChatDAO chatDAO, MessageDAO messageDAO) : base(chatDAO)
        {
            _chatDAO = chatDAO;
            _messageDAO = messageDAO;
        }

        public async Task<IEnumerable<Message>> GetMessagesByConversationAsync(int conversationId)
        {
            return await _chatDAO.GetMessagesByConversationAsync(conversationId);
        }

        public async Task MarkAllMessagesAsReadAsync(int conversationId, string readerId)
        {
            await _chatDAO.MarkAllMessagesAsReadAsync(conversationId, readerId);
        }

        public async Task<Message> AddMessageAsync(Message message)
        {
            return await _messageDAO.AddAsync(message);
        }

        public async Task<Conversation> GetOrCreateConversationAsync(string user1Id, string user2Id)
        {
            return await _chatDAO.GetOrCreateConversationAsync(user1Id, user2Id);
        }

        public async Task<IEnumerable<Conversation>> GetConversationsByCustomerIdAsync(string customerId)
        {
            return await _chatDAO.GetConversationsByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Conversation>> GetConversationsByUserAsync(string userId)
        {
            return await _chatDAO.GetConversationsByUserAsync(userId);
        }
    }
} 