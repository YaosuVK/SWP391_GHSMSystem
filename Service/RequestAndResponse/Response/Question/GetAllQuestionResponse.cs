using BusinessObject.Model;
using Service.RequestAndResponse.Response.Accounts;
using Service.RequestAndResponse.Response.QnAMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.Question
{
    public class GetAllQuestionResponse
    {
        public int QuestionID { get; set; }

        public string CustomerID { get; set; }
        public GetCustomerUser Customer { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<QnAMessageResponse> Messages { get; set; }
    }
}
