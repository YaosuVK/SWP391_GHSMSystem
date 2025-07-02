using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.CyclePredictions
{
    public class CreateCyclePredictionRequest
    {
        [Required]
        public int MenstrualCycleID { get; set; }

        [Required]
        public DateTime OvulationDate { get; set; }

        [Required]
        public DateTime FertileStartDate { get; set; }

        [Required]
        public DateTime FertileEndDate { get; set; }

        [Required]
        public DateTime NextPeriodStartDate { get; set; }
    }
} 