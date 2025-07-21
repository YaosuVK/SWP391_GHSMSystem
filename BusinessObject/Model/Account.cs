using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Account : IdentityUser
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public bool Status { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public ConsultantProfile? ConsultantProfile { get; set; }

        public ICollection<LabTest> LabTests { get; set; }

        public ICollection<Appointment> CustomerAppointments { get; set; }
        public ICollection<Appointment> ConsultantAppointments { get; set; }

        public ICollection<TreatmentOutcome> CustomerTreatmentOutcomes { get; set; }

        public ICollection<TreatmentOutcome> ConsultantTreatmentOutcomes { get; set; }

        public ICollection<ConsultantSlot> ConsultantSlots { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        
        public ICollection<FeedBack> Ratings { get; set; }

        public ICollection<Services> Services { get; set; }

        public ICollection<MenstrualCycle> MenstrualCycles { get; set; }

        public ICollection<QnAMessage> QnACustomerMessages { get; set; } // Add for Q&A customer messages
        public ICollection<QnAMessage> QnAConsultantMessages { get; set; } // Add for Q&A consultant messages

        public ICollection<Message> SentChatMessages { get; set; } // Add for chat messages sent by this account
        public ICollection<Message> ReceivedChatMessages { get; set; } // Add for chat messages received by this account

        // Add navigation properties for Conversation entity
        public ICollection<Conversation> ConversationsAsUser1 { get; set; } // Conversations where this account is User1
        public ICollection<Conversation> ConversationsAsUser2 { get; set; } // Conversations where this account is User2

        public ICollection<Notification> Notifications { get; set; }
    }
}
