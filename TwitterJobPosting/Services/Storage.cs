using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterJobPosting.Entities;

namespace TwitterJobPosting.Services
{
    public static class Storage
    {
        public static List<Job> Jobs = new List<Job>()
        {
            new Job() { Id = 1, EmployerId = 1, Title = "Construction Supervisor" },
            new Job() { Id = 2, EmployerId = 2, Title = "Associate Project Manager" },
            new Job() { Id = 3, EmployerId = 3, Title = "Project General Manager" },
        };

        public static List<Employer> Employers = new List<Employer>()
        {
            new Employer() { Id = 1, StateId = 1 },
            new Employer() { Id = 2, StateId = 2 },
            new Employer() { Id = 3, StateId = 3 },
        };

        public static List<State> States = new List<State>()
        {
            new State(){ Id = 1, Name = "Alaska" },
            new State(){ Id = 2, Name = "Colorado" },
            new State(){ Id = 3, Name = "Texas" },
        };
    }
}