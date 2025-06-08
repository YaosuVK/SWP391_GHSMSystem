using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Slot
    {
        [Key]
        public int SlotID { get; set; }

        [ForeignKey("ClinicID")]
        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }

        [ForeignKey("WorkingHourID")]
        public int WorkingHourID { get; set; }
        public WorkingHour WorkingHour { get; set; }

        public int MaxConsultant { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public ICollection<ConsultantSlot> ConsultantSlots { get; set; }
    }
}
