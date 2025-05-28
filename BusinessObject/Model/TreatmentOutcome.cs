using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class TreatmentOutcome
    {
        [Key]
        public int TreatmentID { get; set; }

        [ForeignKey("CustomerID")]
        public string CustomerID { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("ConsultantID")]
        public string ConsultantID { get; set; }
        public Consultant Consultant { get; set; }

        [ForeignKey("AppointmentID")]
        public int? AppointmentID { get; set; }
        public Appointment Appointment { get; set; }

        public string Diagnosis { get; set; }

        public string TreatmentPlan { get; set; }

        public string Prescription { get; set; }

        public string Recommendation { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public ICollection<LabTest> LabTests { get; set; }
    }
}
