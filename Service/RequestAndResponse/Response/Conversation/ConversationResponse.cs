using System;
using System.Collections.Generic;

namespace Service.RequestAndResponse.Response.Conversation
{
    public class ConversationResponse
    {
        public int ConversationID { get; set; }
        public string User1ID { get; set; }
        public string User2ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastMessageAt { get; set; }
        public SimplifiedAccountResponse User1 { get; set; }
        public SimplifiedAccountResponse User2 { get; set; }
    }
} 