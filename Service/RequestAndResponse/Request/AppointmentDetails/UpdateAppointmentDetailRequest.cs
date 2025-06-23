using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.AppointmentDetails
{
    public class UpdateAppointmentDetailRequest
    {
        public int? AppointmentDetailID { get; set; }

        public int? ServicesID { get; set; }

        public int? ConsultantProfileID { get; set; }

        public int Quantity { get; set; }
    }
}
