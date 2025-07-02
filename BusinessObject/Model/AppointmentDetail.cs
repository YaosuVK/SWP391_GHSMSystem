using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class AppointmentDetail
    {
        [Key]
        public int AppointmentDetailID { get; set; }

        [ForeignKey("AppointmentID")]
        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; }

        
        public int? ServicesID { get; set; }
        [ForeignKey("ServicesID")]
        public Services Service { get; set; }

        public int? ConsultantProfileID { get; set; }
        [ForeignKey("ConsultantProfileID")]
        public ConsultantProfile ConsultantProfile { get; set; }

        public int Quantity { get; set; }

        public double ServicePrice { get; set; }

        public double TotalPrice { get; set; }
    }
}
