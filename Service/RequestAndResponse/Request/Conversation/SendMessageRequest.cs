using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Service.RequestAndResponse.Request.Conversation
{
    public class SendMessageRequest
    {
        [Required(ErrorMessage = "ReceiverID is required.")]
        public string ReceiverID { get; set; }

        [Required(ErrorMessage = "SenderID is required.")]
        public string SenderID { get; set; }

        [RegularExpression(@"^(?!\\s*$).+", ErrorMessage = "Content cannot be empty or contain only whitespace.")]
        public string? Content { get; set; }

        public List<IFormFile>? Images { get; set; }
    }
} 