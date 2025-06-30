using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        Task<Question> AddAsync(Question q);
        Task<Question> GetByIdAsync(int id);
        Task<IEnumerable<Question>> GetAllAsync();
    }
}
