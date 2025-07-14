using BusinessObject.Model;
using Service.RequestAndResponse.Response.ConsultantProfiles;
using Service.RequestAndResponse.Response.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.AppointmentDetails
{
    public class GetAppointmentDetailResponse
    {
        public int AppointmentDetailID { get; set; }

        public int? ServicesID { get; set; }
        public GetServiceResponse Service { get; set; }

        public int? ConsultantProfileID { get; set; }
        public GetConsultantProfileResponse ConsultantProfile { get; set; }

        public int Quantity { get; set; }

        public double ServicePrice { get; set; }

        public double TotalPrice { get; set; }
    }
}
