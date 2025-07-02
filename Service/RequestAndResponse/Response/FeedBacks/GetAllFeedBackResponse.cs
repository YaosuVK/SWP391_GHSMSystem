using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.FeedBacks
{
    public class GetAllFeedBackResponse
    {
        public List<FeedBackResponse> FeedBacks { get; set; } = new List<FeedBackResponse>();
        public int TotalCount { get; set; }
    }
} 