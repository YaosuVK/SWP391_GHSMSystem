using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class ConsultantProfile
    {
        [Key]
        public int ConsultantProfileID { get; set; }

        public string AccountID { get; set; }
        [ForeignKey("AccountID")]
        public Account Account { get; set; }

        public string? Description { get; set; }

        public string? Specialty { get; set; }

        public string? Experience { get; set; }

        public double ConsultantPrice { get; set; }

        public ICollection<AppointmentDetail> AppointmentDetails { get; set; }
    }
}
