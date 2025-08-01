﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.Accounts
{
    public class LoginResponse
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool? isActive { get; set; }
        public IList<string> Roles { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
