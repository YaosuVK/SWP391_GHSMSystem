using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Notifications
{
    public class NotificationRequest
    {
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int Type { get; set; } 
    }
}
