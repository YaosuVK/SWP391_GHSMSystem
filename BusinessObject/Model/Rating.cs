using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Rating
    {
        [Key]
        public int RatingID { get; set; }

        [ForeignKey("CustomerID")]
        public string CustomerID { get; set; }
        public Account Customer { get; set; }

        public double SumRate { get; set; }

        public double ServiceRate { get; set; }

        public double FacilityRate { get; set; }

        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
