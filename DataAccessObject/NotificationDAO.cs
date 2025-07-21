using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class NotificationDAO : BaseDAO<Notification>
    {
        private readonly GHSMContext _context;

        public NotificationDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationByAccountID( string accountID)
        {
            return await _context.Notifications
                .Include(n => n.Customer)
                .OrderByDescending(n => n.CreatedAt)
                .Where(n => n.CustomerID == accountID)
                .ToListAsync();
        }

        public async Task<Notification?> GetNotificationByIdAsync(int notificationId)
        {
            return await _context.Notifications
            .Include(n => n.Customer)
                .FirstOrDefaultAsync(n => n.Id == notificationId);
        }

        public async Task<Notification> MarkNotificationAsReadAsync(int notificationId)
        {
            var notification = await GetByIdAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await UpdateAsync(notification);
            }
            return notification;
        }

        public async Task MarkAllNotificationsAsReadAsync(string accountId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.CustomerID == accountId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await UpdateRange(notifications);
        }
    }
}
