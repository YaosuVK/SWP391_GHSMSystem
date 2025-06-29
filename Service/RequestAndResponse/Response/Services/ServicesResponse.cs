using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.RequestAndResponse.Response.ImageService;
using Service.RequestAndResponse.Response.Categories;

namespace Service.RequestAndResponse.Response.Services
{
    public class ServicesResponse
    {
        public int ServicesID { get; set; }
        
        public int CategoryID { get; set; }
        public GetCategoryForService Category { get; set; }

        public string ServicesName { get; set; }

        public string Description { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public double ServicesPrice { get; set; }

        public bool Status { get; set; }

        public ICollection<ImageServiceResponse> ImageServices { get; set; }
    }
}
