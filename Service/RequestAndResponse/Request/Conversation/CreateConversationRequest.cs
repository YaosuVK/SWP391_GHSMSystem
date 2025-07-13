using System;
using System.ComponentModel.DataAnnotations;

namespace Service.RequestAndResponse.Request.Conversation
{
    public class CreateConversationRequest
    {
        [Required(ErrorMessage = "ReceiverID is required.")]
        public string ReceiverID { get; set; }

        [Required(ErrorMessage = "SenderID is required.")]
        public string SenderID { get; set; }

        public DateTime CreatedAt { get; set; }
    }
} 