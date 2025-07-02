using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.ConsultantProfiles
{
    public class UpdateConsultantProfile
    {
        public string? Description { get; set; }

        public string? Specialty { get; set; }

        public string? Experience { get; set; }

        public double ConsultantPrice { get; set; }
    }
}
