using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.CyclePredictions
{
    public class UpdateCyclePredictionRequest
    {
        [Required]
        public int CyclePredictionID { get; set; }

        public int? MenstrualCycleID { get; set; }

        public DateTime? OvulationDate { get; set; }

        public DateTime? FertileStartDate { get; set; }

        public DateTime? FertileEndDate { get; set; }

        public DateTime? NextPeriodStartDate { get; set; }
    }
} 