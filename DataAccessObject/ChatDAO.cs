using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class ChatDAO : BaseDAO<Conversation>
    {
        private readonly GHSMContext _context;

        public ChatDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Conversation>> GetConversationsByUserAsync(string userId)
        {
            return await _context.Conversations
                .Where(c => c.User1ID == userId || c.User2ID == userId)
                .Include(c => c.User1)
                .Include(c => c.User2)
                .OrderByDescending(c => c.LastMessageAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetMessagesByConversationAsync(int conversationId)
        {
            return await _context.Messages
                .Where(m => m.ConversationID == conversationId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task MarkAllMessagesAsReadAsync(int conversationId, string readerId)
        {
            var messagesToMark = await _context.Messages
                .Where(m => m.ConversationID == conversationId && m.ReceiverID == readerId && !m.IsRead)
                .ToListAsync();

            foreach (var message in messagesToMark)
            {
                message.IsRead = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Conversation> GetOrCreateConversationAsync(string user1Id, string user2Id)
        {
            var conversation = await _context.Conversations
                .Include(c => c.User1)
                .Include(c => c.User2)
                .FirstOrDefaultAsync(c =>
                    (c.User1ID == user1Id && c.User2ID == user2Id) ||
                    (c.User1ID == user2Id && c.User2ID == user1Id));

            if (conversation == null)
            {
                conversation = new Conversation
                {
                    User1ID = user1Id,
                    User2ID = user2Id,
                    CreatedAt = DateTime.UtcNow,
                    LastMessageAt = DateTime.UtcNow
                };
                await _context.Conversations.AddAsync(conversation);
                await _context.SaveChangesAsync();
            }
            return conversation;
        }

        public async Task<IEnumerable<Conversation>> GetConversationsByCustomerIdAsync(string customerId)
        {
            return await _context.Conversations
                .Where(c => c.User1ID == customerId || c.User2ID == customerId)
                .Include(c => c.User1)
                .Include(c => c.User2)
                .OrderByDescending(c => c.LastMessageAt)
                .ToListAsync();
        }
    }
} 