using Service.RequestAndResponse.Response.Accounts;
using Service.RequestAndResponse.Response.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.ConsultantSlots
{
    public class ConsultantSlotResponse
    {
        public string ConsultantID { get; set; }
        public GetConsultantUser Consultant { get; set; }

        public int SlotID { get; set; }
        public GetSlotResponse Slot { get; set; }

        public int MaxAppointment { get; set; }

        public DateTime AssignedDate { get; set; }

    }
}
