using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Categories
{
    public class UpdateCategoryRequest
    {
        public string Name { get; set; }

        public bool Status { get; set; }
    }
}
