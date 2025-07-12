using BusinessObject.Model;
using Service.RequestAndResponse.Response.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.ConsultantSlots
{
    public class GetConsultantSlotForProfile
    {
        public int SlotID { get; set; }
        public GetSlotResponseForProfile Slot { get; set; }

        public int MaxAppointment { get; set; }

        // Bạn có thể thêm các trường phụ nếu cần, ví dụ:
        public DateTime AssignedDate { get; set; }
    }
}
