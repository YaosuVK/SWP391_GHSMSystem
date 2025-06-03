using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Customer
    {
        [Key, ForeignKey("Account")]
        public string CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string Email { get; set; }

        public string? Password { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public bool Status { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public Account Account { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<TreatmentOutcome> TreatmentOutcomes { get; set; }
        public ICollection<LabTest> LabTests { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<MenstrualCycle> MenstrualCycles { get; set; }
    }
}
