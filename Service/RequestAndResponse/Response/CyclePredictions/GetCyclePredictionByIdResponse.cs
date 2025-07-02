using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.CyclePredictions
{
    public class GetCyclePredictionByIdResponse
    {
        public int CyclePredictionID { get; set; }
        public int MenstrualCycleID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime OvulationDate { get; set; }
        public DateTime FertileStartDate { get; set; }
        public DateTime FertileEndDate { get; set; }
        public DateTime NextPeriodStartDate { get; set; }
        public DateTime CycleStartDate { get; set; }
        public int CycleLength { get; set; }
        public int PeriodLength { get; set; }
        public int FertileWindowDays { get; set; }
        public int DaysUntilNextPeriod { get; set; }
    }
} 