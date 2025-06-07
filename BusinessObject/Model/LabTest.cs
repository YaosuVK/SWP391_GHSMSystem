using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class LabTest
    {
        [Key]
        public int LabTestID { get; set; }

        public string CustomerID { get; set; }
        public Account Customer { get; set; }

        public string StaffID { get; set; }
        public Account Staff { get; set; }

        [ForeignKey("TreatmentID")]
        public int? TreatmentID { get; set; }
        public TreatmentOutcome TreatmentOutcome { get; set; }

        public string TestName { get; set; }
        
        public string Result { get; set; }
        
        public string ReferenceRange { get; set; }
        
        public string Unit { get; set; }
        
        public bool? IsPositive { get; set; }
        
        public DateTime TestDate { get; set; }
    }
}
