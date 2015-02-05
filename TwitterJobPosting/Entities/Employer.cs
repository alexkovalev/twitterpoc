using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterJobPosting.Entities
{
    public class Employer
    {
        public Employer() { }

        public int Id { get; set; }
        public int StateId { get; set; }
    }
}