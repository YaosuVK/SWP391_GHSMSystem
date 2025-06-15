using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.Slots
{
    public class SlotResponse
    {
        public int SlotID { get; set; }

        public int MaxConsultant { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public ICollection<ConsultantSlot> ConsultantSlots { get; set; }
    }
}
