using Service.RequestAndResponse.Request.AppointmentDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Appointments
{
    public class UpdateAppointmentRequest
    {
        public string? ConsultantID { get; set; }

        public int? SlotID { get; set; }

        [Required(ErrorMessage = "AppointmentDate is required.")]
        public DateTime AppointmentDate { get; set; }

        public ICollection<UpdateAppointmentDetailRequest> AppointmentDetails { get; set; }
    }
}
