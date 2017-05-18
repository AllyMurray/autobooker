using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using AutoBooking.Models;
using AutoBooking.Models.Models;

namespace AutoBooking.Booker
{
    public class Functions
    {
        [NoAutomaticTrigger]
        public static async Task BookActivities()
        {
            DocumentDbRepository<Activity>.Initialize();
            DocumentDbRepository<ApplicationUser>.Initialize();

            var bookingDay = DateTime.Today.AddDays(6);
            var activities = (await DocumentDbRepository<Activity>.GetItemsAsync(x => x.Text.Contains(bookingDay.ToString("dddd").Substring(0, 5)))).ToList();

            var activityIds = activities.Select(activity => activity.Id);
            var users = (await DocumentDbRepository<ApplicationUser>.GetAllItemsAsync())
                .Where(user => user.ActivityIds.Intersect(activityIds).Any()).ToList();

            users.ForEach(user =>
            {
                var api = new API.Api(user.LeisureBookingUsername, user.LeisureBookingPassword);

                activities.ForEach(activity =>
                {
                    if (user.ActivityIds.Contains(activity.Id))
                    {
                        api.BookActivity(activity.Id);

                        //Handle the response from the booking method.
                        //We will email the user if there is a problem.
                    }
                });

                api.LogOut();
            });


        }
    }
}
