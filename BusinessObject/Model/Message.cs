using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        public int QuestionID { get; set; }
        

        public string? CustomerID { get; set; }
        public string? ConsultantID { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? ParentMessageId { get; set; }     // Trả lời cho message nào (nếu có)

        // Navigation
        public Account Customer { get; set; }
        public Account Consultant { get; set; }
        public Question Question { get; set; }
        public Message ParentMessage { get; set; }
        public ICollection<Message> Replies { get; set; }
    }
}
