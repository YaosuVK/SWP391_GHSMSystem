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
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        private readonly NotificationDAO _notificationDao;
        public NotificationRepository(NotificationDAO notificationDao) : base(notificationDao)
        {
            _notificationDao = notificationDao;
        }

        public async Task<Notification> CreateNotificationAsync(Notification notification)
        {
            return await _notificationDao.AddAsync(notification);
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationByAccountID(string accountID)
        {
            return await _notificationDao.GetAllNotificationByAccountID(accountID);
        }

        public async Task<Notification?> GetNotificationByIdAsync(int notificationId)
        {
            return await _notificationDao.GetNotificationByIdAsync(notificationId);
        }

        public async Task MarkAllNotificationsAsReadAsync(string accountId)
        {
            await _notificationDao.MarkAllNotificationsAsReadAsync(accountId);
        }

        public async Task<Notification> MarkNotificationAsReadAsync(int notificationId)
        {
            return await _notificationDao.MarkNotificationAsReadAsync(notificationId);
        }
    }
}
