﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.VnPayModel
{
    public class VnPayRequestModel
    {
        public int? AppointmentID { get; set; }
        public string? AccountID { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }

        public string OrderInfor { get; set; }
    }
}
