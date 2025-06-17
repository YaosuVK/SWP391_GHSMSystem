using BusinessObject.Model;
using Service.RequestAndResponse.Response.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.Appointments
{
    public class GetAllAppointment
    {
        public int AppointmentID { get; set; }

        public string CustomerID { get; set; }
        public Account Customer { get; set; }

        public string? ConsultantID { get; set; }
        public Account Consultant { get; set; }

        public int? TreatmentID { get; set; }
        public TreatmentOutcome TreatmentOutcome { get; set; }

        public int? SlotID { get; set; }
        public Slot Slot { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public DateTime UpdateAt { get; set; }

        public double TotalAmount { get; set; }

        public AppointmentStatus Status { get; set; }

        public AppointmentType AppointmentType { get; set; }

        public PaymentStatus paymentStatus { get; set; }
    }
}
