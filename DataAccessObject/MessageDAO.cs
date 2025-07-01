using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class MessageDAO : BaseDAO<Message>
    {
        private readonly GHSMContext _context;

        public MessageDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Message> AddAsync(Message entity)
        {
            await _context.Messages.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Message>> GetByQuestionAsync(int questionId)
        {
            return await _context.Messages
                .Where(m => m.QuestionID == questionId && m.ParentMessageId == null)
                .Include(m => m.Replies)
                .ToListAsync();
        }

        public async Task<Message> GetByIdAsync(int messageId)
        {
            return await _context.Messages
                .Include(m => m.Replies)
                .FirstOrDefaultAsync(m => m.MessageID == messageId);
        }
    }
}
