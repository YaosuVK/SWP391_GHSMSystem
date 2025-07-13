using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
