using System.Collections.Generic;

namespace Service.RequestAndResponse.Response.Conversation
{
    public class ConversationWithMessagesResponse
    {
        public int ConversationID { get; set; }
        public SimplifiedAccountResponse? OtherUser { get; set; }
        public SimplifiedMessageResponse? LastMessage { get; set; }
        public List<SimplifiedMessageResponse>? Messages { get; set; }
    }
} 