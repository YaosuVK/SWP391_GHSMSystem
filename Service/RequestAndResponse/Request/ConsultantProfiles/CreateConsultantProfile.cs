using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.ConsultantProfiles
{
    public class CreateConsultantProfile
    {
        [Required(ErrorMessage = "ConsultantID is required.")]
        public string AccountID { get; set; }

        public string? Description { get; set; }

        public string? Specialty { get; set; }

        public string? Experience { get; set; }

        public double ConsultantPrice { get; set; }
    }
}
