using Service.RequestAndResponse.Response.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.ConsultantProfiles
{
    public class GetAllConsultantProfile
    {
        public int ConsultantProfileID { get; set; }

        public GetConsultantUserForProfile Account { get; set; }

        public string? Description { get; set; }

        public string? Specialty { get; set; }

        public string? Experience { get; set; }

        public double ConsultantPrice { get; set; }
    }
}
