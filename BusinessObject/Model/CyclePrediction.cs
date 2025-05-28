using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class CyclePrediction
    {
        [Key]
        public int CyclePredictionID { get; set; }

        [ForeignKey("MenstrualCycleID")]
        public int MenstrualCycleID { get; set; }
        public MenstrualCycle MenstrualCycle { get; set; }

        public DateTime OvulationDate { get; set; }
        
        public DateTime FertileStartDate { get; set; }
        
        public DateTime FertileEndDate { get; set; }
        
        public DateTime NextPeriodStartDate { get; set; }
    }
}
