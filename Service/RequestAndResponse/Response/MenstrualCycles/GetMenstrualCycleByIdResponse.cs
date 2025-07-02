using Service.RequestAndResponse.Response.CyclePredictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.MenstrualCycles
{
    public class GetMenstrualCycleByIdResponse
    {
        public int MenstrualCycleID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime StartDate { get; set; }
        public int PeriodLength { get; set; }
        public int CycleLength { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public DateTime EstimatedNextCycleDate { get; set; }
        public List<GetAllCyclePredictionResponse> CyclePredictions { get; set; } = new List<GetAllCyclePredictionResponse>();
    }
} 