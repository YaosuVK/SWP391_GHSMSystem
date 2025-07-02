using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.TreatmentOutcomes
{
    public class UpdateTreatmentOutcomeRequest
    {
        [Required]
        public int TreatmentID { get; set; }

        public string CustomerID { get; set; }

        public string ConsultantID { get; set; }

        public int? AppointmentID { get; set; }

        [StringLength(1000)]
        public string Diagnosis { get; set; }

        [StringLength(2000)]
        public string TreatmentPlan { get; set; }

        [StringLength(1000)]
        public string Prescription { get; set; }

        [StringLength(1000)]
        public string Recommendation { get; set; }
    }
} 