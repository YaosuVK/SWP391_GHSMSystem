using BusinessObject.Model;
using Repository.IBaseRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IQnAMessageRepository : IBaseRepository<QnAMessage>
    {
        Task<QnAMessage> AddAsync(QnAMessage message);
        Task<IEnumerable<QnAMessage>> GetByQuestionAsync(int questionId);
        Task<QnAMessage> GetByIdAsync(int messageId);
    }
} 