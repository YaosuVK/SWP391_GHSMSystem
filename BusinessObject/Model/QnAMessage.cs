using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Model
{
    public class QnAMessage
    {
        [Key]
        public int MessageID { get; set; }

        public int QuestionID { get; set; }

        public string? CustomerID { get; set; }
        public string? ConsultantID { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? ParentMessageId { get; set; }

        // Navigation properties
        [ForeignKey("CustomerID")]
        public Account? Customer { get; set; }
        [ForeignKey("ConsultantID")]
        public Account? Consultant { get; set; }
        [ForeignKey("QuestionID")]
        public Question? Question { get; set; }
        [ForeignKey("ParentMessageId")]
        public QnAMessage? ParentMessage { get; set; }
        public ICollection<QnAMessage>? Replies { get; set; }
    }
} 