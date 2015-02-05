using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterJobPosting.Entities
{
    public class Job
    {
        public Job() { }

        public int Id { get; set; }
        public int EmployerId { get; set; }
        public string Title { get; set; }
    }
}