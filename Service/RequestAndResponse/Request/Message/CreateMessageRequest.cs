using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Message
{
    public class CreateMessageRequest
    {
        public string Content { get; set; }
        public int? ParentMessageId { get; set; }
    }
}
