using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Services
    {
        [Key]
        public int ServicesID { get; set; }

        [ForeignKey("ClinicID")]
        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }

        [ForeignKey("CategoryID")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        [ForeignKey("ManagerID")]
        public string ManagerID { get; set; }
        public Account Manager { get; set; }

        [Required]
        public string ServicesName { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        [Required]
        public double ServicesPrice { get; set; }

        public bool Status { get; set; }

        [EnumDataType(typeof(ServiceType))]
        public ServiceType ServiceType { get; set; }

        public ICollection<AppointmentDetail> AppointmentDetails { get; set; }
        public ICollection<ImageService> ImageServices { get; set; }
    }

    public enum ServiceType
    {
        Consultation = 0,
        TestingSTI = 1
    }
}
