using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.RequestAndResponse.Response.Clinic;
using Service.RequestAndResponse.Response.TreatmentOutcomes;
using Service.RequestAndResponse.Response.Accounts;
using Service.RequestAndResponse.Response.Slots;

namespace Service.RequestAndResponse.Response.Appointments
{
    public class GetAppointmentResponse
    {
        public int AppointmentID { get; set; }

        public string CustomerID { get; set; }
        public GetCustomerUser Customer { get; set; }

        public string? ConsultantID { get; set; }
        public GetConsultantUser Consultant { get; set; }

        public int ClinicID { get; set; }
        public ClinicResponse Clinic { get; set; }

        public int? TreatmentID { get; set; }
        public GetTreatmentOutcomeResponse TreatmentOutcome { get; set; }

        public int? SlotID { get; set; }
        public GetSlotResponse Slot { get; set; }

        public string AppointmentCode { get; set; }

        public DateTime ExpiredTime { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public DateTime UpdateAt { get; set; }

        public double TotalAmount { get; set; }

        public AppointmentStatus Status { get; set; }

        public AppointmentType AppointmentType { get; set; }

        public PaymentStatus paymentStatus { get; set; }
    }
}
