using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Model
{
    public class Conversation
    {
        [Key]
        public int ConversationID { get; set; }

        [Required]
        public string User1ID { get; set; }

        [Required]
        public string User2ID { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastMessageAt { get; set; }

        // Navigation properties
        [ForeignKey("User1ID")]
        public Account User1 { get; set; }

        [ForeignKey("User2ID")]
        public Account User2 { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
} 