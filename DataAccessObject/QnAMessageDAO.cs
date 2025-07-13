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
    public class QnAMessageDAO : BaseDAO<QnAMessage>
    {
        private readonly GHSMContext _context;

        public QnAMessageDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<QnAMessage> AddAsync(QnAMessage entity)
        {
            await _context.QnAMessages.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<QnAMessage>> GetByQuestionAsync(int questionId)
        {
            return await _context.QnAMessages
                .Where(m => m.QuestionID == questionId && m.ParentMessageId == null)
                .Include(m => m.Replies)
                .ToListAsync();
        }

        public async Task<QnAMessage> GetByIdAsync(int messageId)
        {
            return await _context.QnAMessages
                .Include(m => m.Replies)
                .FirstOrDefaultAsync(m => m.MessageID == messageId);
        }
    }
} 