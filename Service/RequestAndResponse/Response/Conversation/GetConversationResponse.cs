using System;

namespace Service.RequestAndResponse.Response.Conversation
{
    public class GetConversationResponse
    {
        public int ConversationID { get; set; }
        public SimplifiedAccountResponse? OtherUser { get; set; }
        public SimplifiedMessageResponse? LastMessage { get; set; }
    }
} 