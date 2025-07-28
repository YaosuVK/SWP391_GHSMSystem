using BusinessObject.Model;
using Service.RequestAndResponse.Response.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.QnAMessages
{
    public class QnAMessageResponse
    {
        public int MessageID { get; set; }

        public int QuestionID { get; set; }

        public string? CustomerID { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? ParentMessageId { get; set; }

        public GetCustomerUser? Customer { get; set; }

        public ICollection<QnAMessageResponse>? Replies { get; set; }
    }
}
