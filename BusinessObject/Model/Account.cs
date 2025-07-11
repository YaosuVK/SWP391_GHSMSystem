﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Account : IdentityUser
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public bool Status { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public ConsultantProfile? ConsultantProfile { get; set; }

        public ICollection<LabTest> LabTests { get; set; }

        public ICollection<Appointment> CustomerAppointments { get; set; }
        public ICollection<Appointment> ConsultantAppointments { get; set; }

        public ICollection<TreatmentOutcome> CustomerTreatmentOutcomes { get; set; }

        public ICollection<TreatmentOutcome> ConsultantTreatmentOutcomes { get; set; }

        public ICollection<ConsultantSlot> ConsultantSlots { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        
        public ICollection<FeedBack> Ratings { get; set; }

        public ICollection<Services> Services { get; set; }

        public ICollection<MenstrualCycle> MenstrualCycles { get; set; }
    }
}
