using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.Message
{
    public class MessageResponse
    {
        public int MessageID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? CustomerID { get; set; }
        public string? ConsultantID { get; set; }

        // Nested replies
        public List<MessageResponse> Replies { get; set; }
    }
}
