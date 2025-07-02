using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.FeedBacks
{
    public class UpdateFeedBackRequest
    {
        [Required]
        [Range(1.0, 5.0, ErrorMessage = "ServiceRate must be between 1.0 and 5.0")]
        public double ServiceRate { get; set; }

        [Required]
        [Range(1.0, 5.0, ErrorMessage = "FacilityRate must be between 1.0 and 5.0")]
        public double FacilityRate { get; set; }

        public string? Content { get; set; }
    }
} 