using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class MenstrualCycle
    {

        [Key]
        public int MenstrualCycleID { get; set; }

        [ForeignKey("CustomerID")]
        public string CustomerID { get; set; }
        public Account Customer { get; set; }

        public DateTime StartDate { get; set; }

        public int PeriodLength { get; set; } // Số ngày có kinh

        public int CycleLength { get; set; }  // Độ dài chu kỳ
    }
}
