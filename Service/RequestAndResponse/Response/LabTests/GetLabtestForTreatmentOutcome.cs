using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.LabTests
{
    public class GetLabtestForTreatmentOutcome
    {
        public int LabTestID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public string StaffEmail { get; set; }
        public int? TreatmentID { get; set; }
        public string TreatmentDiagnosis { get; set; }
        public string TestName { get; set; }
        public string Result { get; set; }
        public string ReferenceRange { get; set; }
        public string Unit { get; set; }
        public bool? IsPositive { get; set; }
        public DateTime TestDate { get; set; }
    }
}
