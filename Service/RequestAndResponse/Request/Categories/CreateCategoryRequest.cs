﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Categories
{
    public class CreateCategoryRequest
    {
        public int ClinicID { get; set; }

        public string Name { get; set; }
    }
}
