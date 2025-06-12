using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Clinic
    {
        [Key]
        public int ClinicID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        [EnumDataType(typeof(ClinicStatus))]
        public ClinicStatus Status { get; set; }

        public ICollection<Category> Categories { get; set; }
        public ICollection<WorkingHour> WorkingHours { get; set; }
        public ICollection<Slot> Slots { get; set; }
        public ICollection<Services> Services { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }

    public enum ClinicStatus
    {
        Pending = 0,
        Active = 1,
        InActive = 2
    }

}
