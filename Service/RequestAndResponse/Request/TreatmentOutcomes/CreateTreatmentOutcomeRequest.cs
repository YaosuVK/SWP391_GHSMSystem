using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.TreatmentOutcomes
{
    public class CreateTreatmentOutcomeRequest
    {
        [Required]
        public string CustomerID { get; set; }

        [Required]
        public string ConsultantID { get; set; }

        public int? AppointmentID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Diagnosis { get; set; }

        [Required]
        [StringLength(2000)]
        public string TreatmentPlan { get; set; }

        [StringLength(1000)]
        public string Prescription { get; set; }

        [StringLength(1000)]
        public string Recommendation { get; set; }
    }
} 