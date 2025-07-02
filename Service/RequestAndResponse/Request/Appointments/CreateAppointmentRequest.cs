using BusinessObject.Model;
using Service.RequestAndResponse.Request.AppointmentDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Appointments
{
    public class CreateAppointmentRequest
    {
        [Required(ErrorMessage = "CustomerID is required.")]
        public string CustomerID { get; set; }

        //Set can be null if customer choose a service, than consultant should not be include.
        public string? ConsultantID { get; set; }

        [Required(ErrorMessage = "ClinicID is required.")]
        public int ClinicID { get; set; }

        public int? SlotID { get; set; }

        [Required(ErrorMessage = "AppointmentDate is required.")]
        public DateTime AppointmentDate { get; set; }

        public ICollection<AppointmentDetailRequest> AppointmentDetails { get; set; }
    }
}
