﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Duty { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public Account Account { get; set; }
        public string AccountId { get; set; }
        public Resume Resume { get; set; }
        public int ResumeId { get; set; }
    }
}
