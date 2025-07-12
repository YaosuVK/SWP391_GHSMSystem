using BusinessObject.Model;
using Service.RequestAndResponse.Response.ConsultantSlots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.Accounts
{
    public class GetConsultantUserForProfile
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public bool Status { get; set; }

        public DateOnly DateOfBirth { get; set; }
        
        public ICollection<GetConsultantSlotForProfile> ConsultantSlots { get; set; }
    }
}
