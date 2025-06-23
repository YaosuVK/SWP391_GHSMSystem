using BusinessObject.Model;
using Service.RequestAndResponse.Request.AppointmentDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Appointments
{
    public class CreateAppointmentRequest
    {
        public string CustomerID { get; set; }

        //Set can be null if customer choose a service, than consultant should not be include.
        public string? ConsultantID { get; set; }

        public int ClinicID { get; set; }

        public int? SlotID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public ICollection<AppointmentDetailRequest> AppointmentDetails { get; set; }
    }
}
