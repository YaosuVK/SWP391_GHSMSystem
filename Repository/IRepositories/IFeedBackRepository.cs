using BusinessObject.Model;
using Repository.IBaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IFeedBackRepository : IBaseRepository<FeedBack>
    {
        Task<List<FeedBack>> GetAllFeedBacksAsync();
        Task<FeedBack?> GetFeedBackByIdAsync(int feedBackId);
        Task<List<FeedBack>> GetFeedBacksByCustomerIdAsync(string customerId);
        Task<List<FeedBack>> GetFeedBacksByAppointmentIdAsync(int appointmentId);
        Task<FeedBack?> GetFeedBackByCustomerAndAppointmentAsync(string customerId, int appointmentId);
        Task<bool> ExistsFeedBackByCustomerAndAppointmentAsync(string customerId, int appointmentId);
        Task<FeedBack> CreateFeedBackAsync(FeedBack feedBack);
        Task<FeedBack?> UpdateFeedBackAsync(FeedBack feedBack);
        Task<bool> DeleteFeedBackAsync(int feedBackId);
    }
} 