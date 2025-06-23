using Service.RequestAndResponse.Request.AppointmentDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Appointments
{
    public class UpdateAppointmentRequest
    {
        public string? ConsultantID { get; set; }

        public int? SlotID { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public ICollection<UpdateAppointmentDetailRequest> AppointmentDetails { get; set; }
    }
}
