using System.ComponentModel.DataAnnotations;

namespace Service.RequestAndResponse.Request.Conversation
{
    public class MarkAsReadRequest
    {
        [Required]
        public int ConversationId { get; set; }
        [Required]
        public string SenderId { get; set; }
    }
} 