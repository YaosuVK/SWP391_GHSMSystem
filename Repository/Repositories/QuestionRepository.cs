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
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        private readonly QuestionDAO _questionDAO;
        public QuestionRepository(QuestionDAO questionDao) : base(questionDao)
        {
            _questionDAO = questionDao;
        }
        public async Task<Question> AddAsync(Question q)
        {
            return await _questionDAO.AddAsync(q);
        }
        public async Task<Question> GetByIdAsync(int id)
        {
            return await _questionDAO.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await _questionDAO.GetAllAsync();
        }

    }
}
