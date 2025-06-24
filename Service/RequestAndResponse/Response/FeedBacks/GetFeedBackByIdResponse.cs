using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.FeedBacks
{
    public class GetFeedBackByIdResponse
    {
        public FeedBackResponse? FeedBack { get; set; }

        /*public int FeedBackID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public double SumRate { get; set; }
        public double ServiceRate { get; set; }
        public double FacilityRate { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }*/
    }
} 