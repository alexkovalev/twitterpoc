using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LinqToTwitter;
using TwitterJobPosting.Entities;
using TwitterJobPosting.Services;

namespace TwitterJobPosting.Controllers
{
    public class HomeController : AsyncController
    {
        public async Task<ActionResult> Index()
        {
            var statuses = await GenerateJobsAtRandomTimes(Storage.Jobs.Count, 3 * 1000, 4 * 1000);

            ViewBag.statuses = statuses;

            return View();
        }

        private async Task<List<string>> GenerateJobsAtRandomTimes(int jobCount, int msMinTime, int msMaxTime)
        {
            var waitTimeRandom = new Random();
            var idRandom = new Random();

            var statuses = new List<string>();
            for (int i = 0; i < jobCount; i++)
            {
                int milliseconds = waitTimeRandom.Next(msMinTime, msMaxTime);

                Thread.Sleep(milliseconds);

                var job = Storage.Jobs[i];

                var id = idRandom.Next(1000);
                var tweet = await TwitterJobPostingService.PostJobAsync(job, id + " Hello! A new job for you. ");

                if (tweet != null)
                {
                    statuses.Add("Status returned: " +
                        "(" + tweet.StatusID + ") " +
                        tweet.User.Name + ", " +
                        tweet.Text + "\n");
                }
            }

            return statuses;
        }
    }
}
