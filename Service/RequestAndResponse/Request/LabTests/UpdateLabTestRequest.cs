using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.LabTests
{
    public class UpdateLabTestRequest
    {
        [Required]
        public int LabTestID { get; set; }

        public string CustomerID { get; set; }

        public string StaffID { get; set; }

        public int? TreatmentID { get; set; }

        [StringLength(200)]
        public string TestName { get; set; }

        [StringLength(500)]
        public string Result { get; set; }

        [StringLength(200)]
        public string ReferenceRange { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        public bool? IsPositive { get; set; }

        public DateTime? TestDate { get; set; }
    }
} 