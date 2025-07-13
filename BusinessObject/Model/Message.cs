using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Model
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        public int ConversationID { get; set; }

        [Required]
        public string SenderID { get; set; }

        [Required]
        public string ReceiverID { get; set; }

        public string Content { get; set; }
        public string? senderName { get; set; }
        public string? receiverName { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; } = false;

        // Navigation properties
        [ForeignKey("ConversationID")]
        public Conversation Conversation { get; set; }

        [ForeignKey("SenderID")]
        public Account Sender { get; set; }

        [ForeignKey("ReceiverID")]
        public Account Receiver { get; set; }
    }
}
