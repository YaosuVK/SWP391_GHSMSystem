using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class ConsultantSlot
    {
        public string ConsultantID { get; set; }
        public Account Consultant { get; set; }

        public int SlotID { get; set; }
        public Slot Slot { get; set; }

        // Bạn có thể thêm các trường phụ nếu cần, ví dụ:
        public DateTime AssignedDate { get; set; }
    }
}
