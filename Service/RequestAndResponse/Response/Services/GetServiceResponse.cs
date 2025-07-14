using BusinessObject.Model;
using Service.RequestAndResponse.Response.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.Services
{
    public class GetServiceResponse
    {
        public int ServicesID { get; set; }

        public string ServicesName { get; set; }

        public string Description { get; set; }

        public double ServicesPrice { get; set; }

        public bool Status { get; set; }

        public ServiceType ServiceType { get; set; }
    }
}
