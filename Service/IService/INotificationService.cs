using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Response.Categories;
using Service.RequestAndResponse.Response.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface INotificationService
    {
        Task<Notification> CreateNotificationAsync(string customerId, string title, string content, NotificationType type);
        Task<BaseResponse<IEnumerable<NotificationResponse>>> GetAllNotificationByAccountID(string accountID);
        Task<Notification> MarkNotificationAsReadAsync(int notificationId);
        Task MarkAllNotificationsAsReadAsync(string accountId);
        Task ProcessRemindersAsync();
    }
}
