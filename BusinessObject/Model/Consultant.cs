using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Consultant
    {
        [Key, ForeignKey("Account")]
        public string ConsultantID { get; set; }

        public string ConsultantName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public bool Status { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public Account Account { get; set; }

        public string Description { get; set; }

        public string Specialty { get; set; }

        public string Experience { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public ICollection<TreatmentOutcome> TreatmentOutcomes { get; set; }

        public ICollection<Slot> Slots { get; set; }
    }
}
