﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.IdentityModel
{
    public class UpdateAccountDto
    {
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Role { get; set; }
    }
}
