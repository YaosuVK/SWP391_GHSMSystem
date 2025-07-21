using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllNotificationByAccountID(string accountID);
        Task<Notification> CreateNotificationAsync(Notification notification);
        Task<Notification?> GetNotificationByIdAsync(int notificationId);
        Task<Notification> MarkNotificationAsReadAsync(int notificationId);
        Task MarkAllNotificationsAsReadAsync(string accountId);
    }
}
