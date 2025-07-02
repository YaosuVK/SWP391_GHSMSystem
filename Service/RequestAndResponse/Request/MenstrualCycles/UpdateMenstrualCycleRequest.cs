using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.MenstrualCycles
{
    public class UpdateMenstrualCycleRequest
    {
        [Required]
        public int MenstrualCycleID { get; set; }

        public string CustomerID { get; set; }

        public DateTime? StartDate { get; set; }

        [Range(1, 15, ErrorMessage = "Period length must be between 1 and 15 days")]
        public int? PeriodLength { get; set; }

        [Range(15, 45, ErrorMessage = "Cycle length must be between 15 and 45 days")]
        public int? CycleLength { get; set; }
    }
} 