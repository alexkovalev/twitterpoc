using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using LinqToTwitter;
using TwitterJobPosting.Entities;

namespace TwitterJobPosting.Services
{
    public static class TwitterJobPostingService
    {
        public static async Task<Status> PostJobAsync(Job job, string message)
        {
            var credentialStore = GetCredentialStore(job);

            var authorizer = new SingleUserAuthorizer()
            {
                CredentialStore = credentialStore
            };

            var twitterContext = new TwitterContext(authorizer);

            string status = FormatMessage(job, message);
            Status tweet = await twitterContext.TweetAsync(status);

            return tweet;
        }

        private static ICredentialStore GetCredentialStore(Job job)
        {
            var employer = Storage.Employers.Find(x => x.Id == job.EmployerId);
            var stateName = Storage.States.Find(x => x.Id == employer.StateId).Name;

            var consumerKey = stateName + "ConsumerKey";
            var consumerSecret = stateName + "ConsumerSecret";
            var accessToken = stateName + "AccessToken";
            var accessTokenSecret = stateName + "AccessTokenSecret";

            var credentialStore = new SingleUserInMemoryCredentialStore()
            {
                ConsumerKey = ConfigurationManager.AppSettings[consumerKey],
                ConsumerSecret = ConfigurationManager.AppSettings[consumerSecret],
                AccessToken = ConfigurationManager.AppSettings[accessToken],
                AccessTokenSecret = ConfigurationManager.AppSettings[accessTokenSecret]
            };

            return credentialStore;
        }

        private static string FormatMessage(Job job, string message)
        {
            string status = message + job.Title;

            return status;
        }
    }
}