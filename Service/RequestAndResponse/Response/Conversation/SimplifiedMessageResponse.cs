using System;

namespace Service.RequestAndResponse.Response.Conversation
{
    public class SimplifiedMessageResponse
    {
        public int MessageID { get; set; }
        public int ConversationID { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string Content { get; set; }
        public string? SenderName { get; set; }
        public string? ReceiverName { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
} 