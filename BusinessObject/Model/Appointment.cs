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

        [ForeignKey("CustomerID")]
        public string CustomerID { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("ConsultantID")]
        public string ConsultantID { get; set; }
        public Consultant Consultant { get; set; }

        [ForeignKey("ClinicID")]
        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }

        public int? TreatmentID { get; set; }
        public TreatmentOutcome TreatmentOutcome { get; set; }

        [ForeignKey("SlotID")]
        public int SlotID { get; set; }
        public Slot Slot { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public double TotalAmount { get; set; }

        [EnumDataType(typeof(AppointmentStatus))]
        public AppointmentStatus Status { get; set; }

        [EnumDataType(typeof(AppointmentType))]
        public AppointmentType AppointmentType { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<AppointmentDetail> AppointmentDetails { get; set; }
    }

    public enum AppointmentStatus
    {
        Pending = 0,
        Active = 1,
        InActive = 2
    }

    public enum AppointmentType
    {
        Consulting = 0,
        Curing = 1
    }
}
