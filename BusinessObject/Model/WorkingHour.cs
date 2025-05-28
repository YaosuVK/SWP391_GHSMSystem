using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class WorkingHour
    {
        [Key]
        public int WorkingHourID { get; set; }

        [ForeignKey("ClinicID")]
        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }

        [EnumDataType(typeof(DayInWeek))]
        public DayInWeek DayInWeek { get; set; }

        [EnumDataType(typeof(Shift))]
        public Shift Shift { get; set; }

        public TimeOnly OpeningTime { get; set; }

        public TimeOnly ClosingTime { get; set; }

        public bool Status { get; set; }

        public ICollection<Slot> Slots { get; set; }
    }
    public enum DayInWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public enum Shift
    {
        Afternoon,
        AllDay
    }
}
