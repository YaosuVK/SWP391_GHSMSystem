using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.AppointmentDetails
{
    public class AppointmentDetailRequest
    {
        public int? ServicesID { get; set; }

        public int? ConsultantProfileID { get; set; }

        public int Quantity { get; set; }

        public double ServicePrice { get; set; }

        public double TotalPrice { get; set; }
    }
}
