using BusinessObject.Model;
using DataAccessObject;
using Repository.BaseRepository;
using Repository.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class QnAMessageRepository : BaseRepository<QnAMessage>, IQnAMessageRepository
    {
        private readonly QnAMessageDAO _qnAMessageDAO;

        public QnAMessageRepository(QnAMessageDAO qnAMessageDAO) : base(qnAMessageDAO)
        {
            _qnAMessageDAO = qnAMessageDAO;
        }

        public async Task<QnAMessage> AddAsync(QnAMessage message)
        {
            return await _qnAMessageDAO.AddAsync(message);
        }

        public async Task<IEnumerable<QnAMessage>> GetByQuestionAsync(int questionId)
        {
            return await _qnAMessageDAO.GetByQuestionAsync(questionId);
        }

        public async Task<QnAMessage> GetByIdAsync(int messageId)
        {
            return await _qnAMessageDAO.GetByIdAsync(messageId);
        }
    }
} 