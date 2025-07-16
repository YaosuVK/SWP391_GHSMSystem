using BusinessObject.Model;
using Service.RequestAndResponse.Response.LabTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.TreatmentOutcomes
{
    public class GetAllTreatmentOutcomeResponse
    {
        public int TreatmentID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ConsultantID { get; set; }
        public string ConsultantName { get; set; }
        public int? AppointmentID { get; set; }
        public string Diagnosis { get; set; }
        public string TreatmentPlan { get; set; }
        public string Prescription { get; set; }
        public string Recommendation { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int LabTestCount { get; set; }
        public ICollection<GetLabtestForTreatmentOutcome> LabTests { get; set; }
    }
} 