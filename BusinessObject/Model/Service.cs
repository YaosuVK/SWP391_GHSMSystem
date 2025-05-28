using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Service
    {
        [Key]
        public int ServicesID { get; set; }

        [ForeignKey("ClinicID")]
        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }

        [ForeignKey("CategoryID")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        [Required]
        public string servicesName { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        [Required]
        public double servicesPrice { get; set; }

        public bool Status { get; set; }

        public ICollection<AppointmentDetail> AppointmentDetails { get; set; }
    }
}
