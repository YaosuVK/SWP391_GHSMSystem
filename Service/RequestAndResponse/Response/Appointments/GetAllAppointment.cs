using BusinessObject.Model;
using Service.RequestAndResponse.Response.Accounts;
using Service.RequestAndResponse.Response.Clinic;
using Service.RequestAndResponse.Response.Slots;
using Service.RequestAndResponse.Response.TreatmentOutcomes;
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
        public GetCustomerUser Customer { get; set; }

        public string? ConsultantID { get; set; }
        public GetConsultantUser Consultant { get; set; }

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

        public double remainingBalance { get; set; }

        public double ConsultationFee { get; set; } // Tiền tư vấn

        public double STIsTestFee { get; set; }     // Tiền xét nghiệm STI

        public AppointmentStatus Status { get; set; }

        public PaymentStatus paymentStatus { get; set; }
    }
}
