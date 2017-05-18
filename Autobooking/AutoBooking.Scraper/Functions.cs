using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using AutoBooking.API;
using AutoBooking.Models;
using AutoBooking.Models.Models;

namespace AutoBooking.Scraper
{
    public class Functions
    {
        [NoAutomaticTrigger]
        public static async Task UpdateActivities()
        {
            DocumentDbRepository<Activity>.Initialize();
            DocumentDbRepository<ApplicationUser>.Initialize();

            var user = await DocumentDbRepository<ApplicationUser>.GetFirstItemAsync();
            var api = new API.Api(user.LeisureBookingUsername, user.LeisureBookingPassword);
            var scrapedActivities = api.GetActivities();

            var activities = await DocumentDbRepository<Activity>.GetAllItemsAsync();

            PersistActivities(scrapedActivities, activities);
        }

        private static void PersistActivities(IEnumerable<Activity> scrapedActivities, IEnumerable<Activity> activities)
        {
            var tasks = new List<Task>();
            //tasks.AddRange((from scrapedActivity in scrapedActivities.ToList()
            //    let activity = activities.FirstOrDefault(a => a.Id == scrapedActivity.Id)
            //    select activity == null
            //        ? DocumentDbRepository<Activity>.CreateItemAsync(scrapedActivity)
            //        : DocumentDbRepository<Activity>.UpdateItemAsync(scrapedActivity.Id, scrapedActivity)));

            tasks.AddRange(scrapedActivities.ToList()
                .Select(scrapedActivity => new
                {
                    scrapedActivity,
                    activity = activities.FirstOrDefault(a => a.Id == scrapedActivity.Id)
                })
                .Select(x => x.activity == null
                    ? DocumentDbRepository<Activity>.CreateItemAsync(x.scrapedActivity)
                    : DocumentDbRepository<Activity>.UpdateItemAsync(x.scrapedActivity.Id, x.scrapedActivity)));

            Task.WhenAll(tasks);
        }
    }
}
