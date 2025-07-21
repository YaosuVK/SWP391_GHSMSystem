using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.Notifications
{
    public class NotificationResponse
    {
        public int Id { get; set; }
        public string CustomerID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsRead { get; set; }
        public NotificationType TypeNotify { get; set; }
    }
}
