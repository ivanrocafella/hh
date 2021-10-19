﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Models
{
    public class Account : IdentityUser
    {
        public string Avatar { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
    }
}