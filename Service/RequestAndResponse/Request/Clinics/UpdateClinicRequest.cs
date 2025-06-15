using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Clinic
{
    public class UpdateClinicRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public DateTime UpdateAt { get; set; }

        public ClinicStatus Status { get; set; }
    }
}
