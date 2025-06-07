using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }

        public string CustomerID { get; set; }
        public Account Customer { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public ICollection<Message> Messages { get; set; }
    }
}
