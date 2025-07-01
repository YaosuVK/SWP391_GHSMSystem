using BusinessObject.Model;
using DataAccessObject;
using Repository.BaseRepository;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        private readonly MessageDAO _messageDAO;
        public MessageRepository(MessageDAO messageDAO) : base(messageDAO)
        {
            _messageDAO = messageDAO;
        }
        public async Task<Message> AddAsync(Message m)
        {
            return await _messageDAO.AddAsync(m);
        }
        public async Task<IEnumerable<Message>> GetByQuestionAsync(int questionId)
        {
            return await _messageDAO.GetByQuestionAsync(questionId);
        }

        public Task<Message> GetByIdAsync(int messageId) => _messageDAO.GetByIdAsync(messageId);
    }
}
