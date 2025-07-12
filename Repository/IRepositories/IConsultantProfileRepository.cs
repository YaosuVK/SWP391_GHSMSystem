using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IConsultantProfileRepository : IBaseRepository<ConsultantProfile>
    {
        Task<ConsultantProfile?> GetConsultantProfileByAccountID(string accountID);
        Task<ConsultantProfile?> GetConsultantProfileByID(int? consultantProfileID);
        Task<IEnumerable<ConsultantProfile?>> GetAllConsultantProfile();
    }
}
