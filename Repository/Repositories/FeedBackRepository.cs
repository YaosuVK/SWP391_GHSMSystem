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
    public class FeedBackRepository : BaseRepository<FeedBack>, IFeedBackRepository
    {
        private readonly FeedBackDAO _feedBackDAO;

        public FeedBackRepository() : base()
        {
            _feedBackDAO = new FeedBackDAO();
        }

        public async Task<List<FeedBack>> GetAllFeedBacksAsync()
        {
            return await _feedBackDAO.GetAllFeedBacksAsync();
        }

        public async Task<FeedBack?> GetFeedBackByIdAsync(int feedBackId)
        {
            return await _feedBackDAO.GetFeedBackByIdAsync(feedBackId);
        }

        public async Task<List<FeedBack>> GetFeedBacksByCustomerIdAsync(string customerId)
        {
            return await _feedBackDAO.GetFeedBacksByCustomerIdAsync(customerId);
        }

        public async Task<List<FeedBack>> GetFeedBacksByAppointmentIdAsync(int appointmentId)
        {
            return await _feedBackDAO.GetFeedBacksByAppointmentIdAsync(appointmentId);
        }

        public async Task<FeedBack?> GetFeedBackByCustomerAndAppointmentAsync(string customerId, int appointmentId)
        {
            return await _feedBackDAO.GetFeedBackByCustomerAndAppointmentAsync(customerId, appointmentId);
        }

        public async Task<bool> ExistsFeedBackByCustomerAndAppointmentAsync(string customerId, int appointmentId)
        {
            return await _feedBackDAO.ExistsFeedBackByCustomerAndAppointmentAsync(customerId, appointmentId);
        }

        public async Task<FeedBack> CreateFeedBackAsync(FeedBack feedBack)
        {
            // Auto-calculate SumRate
            feedBack.SumRate = feedBack.ServiceRate + feedBack.FacilityRate;
            feedBack.CreatedAt = DateTime.Now;
            feedBack.UpdatedAt = DateTime.Now;

            return await _feedBackDAO.CreateAsync(feedBack);
        }

        public async Task<FeedBack?> UpdateFeedBackAsync(FeedBack feedBack)
        {
            var existingFeedBack = await _feedBackDAO.GetFeedBackByIdAsync(feedBack.FeedBackID);
            if (existingFeedBack == null)
                return null;

            // Auto-calculate SumRate
            feedBack.SumRate = feedBack.ServiceRate + feedBack.FacilityRate;
            feedBack.UpdatedAt = DateTime.Now;
            feedBack.CreatedAt = existingFeedBack.CreatedAt; // Preserve original creation time

            return await _feedBackDAO.UpdateAsync(feedBack);
        }

        public async Task<bool> DeleteFeedBackAsync(int feedBackId)
        {
            var feedBack = await _feedBackDAO.GetFeedBackByIdAsync(feedBackId);
            if (feedBack == null)
                return false;

            await _feedBackDAO.RemoveAsync(feedBack);
            return true;
        }
    }
} 