using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.RequestAndResponse.Response.Clinic;

namespace Service.RequestAndResponse.Response.WorkingHours
{
    public class WorkingHourResponse
    {
        public int WorkingHourID { get; set; }
       
        public DayInWeek DayInWeek { get; set; }

        public Shift Shift { get; set; }

        public TimeOnly OpeningTime { get; set; }

        public TimeOnly ClosingTime { get; set; }

        public bool Status { get; set; }

        public ICollection<Slot> Slots { get; set; }
    }
}
