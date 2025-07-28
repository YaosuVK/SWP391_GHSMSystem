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
    public class QuestionDAO : BaseDAO<Question>
    {
        private readonly GHSMContext _context;

        public QuestionDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Question> AddAsync(Question entity)
        {
            await _context.Questions.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            return await _context.Questions
                .Include(q => q.Messages)
                    .ThenInclude(m => m.Replies)
                .Include(q => q.Customer)
                .FirstOrDefaultAsync(q => q.QuestionID == id);
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await _context.Questions
                .Include(q => q.Messages)
                    .ThenInclude(m => m.Replies)
                .Include(q => q.Customer)
                .ToListAsync();
        }
    }
}
