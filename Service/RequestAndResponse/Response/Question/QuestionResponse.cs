using Service.RequestAndResponse.Response.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.Question
{
    public class QuestionResponse
    {
        public int QuestionID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // Chứa danh sách messages con
        public List<MessageResponse> Messages { get; set; }
    }
}
