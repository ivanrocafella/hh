using hh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.ViewModels
{
    public class ResumeViewModel
    {
        public Resume Resume { get; set; }
        public Job Job { get; set; }
        public Education Education { get; set; }
        public List<Education> Educations { get; set; }
        public List<Job> Jobs { get; set; }
    }
}
