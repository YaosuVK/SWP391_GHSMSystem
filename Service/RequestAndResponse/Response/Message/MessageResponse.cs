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
        public int? ParentMessageId { get; set; }
        public List<MessageResponse> Replies { get; set; }

    }
}
