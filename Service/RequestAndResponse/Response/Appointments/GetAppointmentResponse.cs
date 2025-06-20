﻿using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.RequestAndResponse.Response.Clinic;

namespace Service.RequestAndResponse.Response.Appointments
{
    public class GetAppointmentResponse
    {
        public int AppointmentID { get; set; }

        public string CustomerID { get; set; }
        public Account Customer { get; set; }

        public string? ConsultantID { get; set; }
        public Account Consultant { get; set; }

        public int ClinicID { get; set; }
        public ClinicResponse Clinic { get; set; }

        public int? TreatmentID { get; set; }
        public TreatmentOutcome TreatmentOutcome { get; set; }

        public int? SlotID { get; set; }
        public Slot Slot { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public DateTime UpdateAt { get; set; }

        public double TotalAmount { get; set; }

        public AppointmentStatus Status { get; set; }

        public AppointmentType AppointmentType { get; set; }

        public PaymentStatus paymentStatus { get; set; }
    }
}
