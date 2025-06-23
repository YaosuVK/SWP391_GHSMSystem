using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        
        public string CustomerID { get; set; }
        public Account Customer { get; set; }

        public string? ConsultantID { get; set; }
        public Account Consultant { get; set; }

        [ForeignKey("ClinicID")]
        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }

        public int? TreatmentID { get; set; }
        public TreatmentOutcome TreatmentOutcome { get; set; }

        [ForeignKey("SlotID")]
        public int? SlotID { get; set; }
        public Slot Slot { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public DateTime UpdateAt { get; set; }

        public double TotalAmount { get; set; }

        [EnumDataType(typeof(AppointmentStatus))]
        public AppointmentStatus Status { get; set; }

        [EnumDataType(typeof(AppointmentType))]
        public AppointmentType AppointmentType { get; set; }

        [EnumDataType(typeof(PaymentStatus))]
        public PaymentStatus paymentStatus { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<AppointmentDetail> AppointmentDetails { get; set; }

        public ICollection<FeedBack> FeedBacks { get; set; }
    }

    public enum AppointmentStatus
    {
        Pending = 0,
        Confirmed = 1,
        InProgress = 2,
        WaitingForResult = 3,
        Completed = 4,
        Cancelled = 5,
        RequestRefund = 6,
        RequestCancel = 7
    }

    public enum PaymentStatus
    {
        Pending = 0,
        Deposited = 1,
        FullyPaid = 2,
        Refunded = 3
    }

    public enum AppointmentType
    {
        Consulting = 0,
        Testing = 1
    }
}
