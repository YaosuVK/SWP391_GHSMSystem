using Service.RequestAndResponse.Request.AppointmentDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Appointments
{
    public class UpdateApppointmentRequestSTI
    {
        public ICollection<UpdateAppointmentDetailSTI> AppointmentDetails { get; set; }
    }
}
