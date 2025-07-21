using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Notification
    {
            [Key]
            public int Id { get; set; }

            public string CustomerID { get; set; }
            public Account Customer { get; set; }

            public string Title { get; set; }

            public string Content { get; set; }

            public DateTime CreatedAt { get; set; }

            public bool IsRead { get; set; }

            [EnumDataType(typeof(NotificationType))]
            public NotificationType Type { get; set; }        
    }

    public enum NotificationType
    {
        MenstrualCycle,
        Ovulation,
        FertileWindow,
        MedicationReminder,
        AppointmentReminder,
        VaccineReminder,
        Other
    }
}
