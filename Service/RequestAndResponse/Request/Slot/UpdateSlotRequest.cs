using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Slot
{
    public class UpdateSlotRequest
    {
        public int WorkingHourID { get; set; }

        public int MaxConsultant { get; set; }

        public int MaxTestAppointment { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
