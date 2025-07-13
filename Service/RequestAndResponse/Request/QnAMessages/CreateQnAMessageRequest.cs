namespace Service.RequestAndResponse.Request.QnAMessages
{
    public class CreateQnAMessageRequest
    {
        public string Content { get; set; }
        public int? ParentMessageId { get; set; }
    }
} 