using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoBooking.API;
using AutoBooking.Models;
using AutoBooking.Models.Models;
using AutoBooking.WebUI.Utilities;
using AutoBooking.WebUI.ViewModels;

namespace AutoBooking.WebUI.Controllers
{
    public class BookingController : Controller
    {
        [OutputCache(Duration = 86400, VaryByParam = "none")]
        // GET: Booking
        public async Task<ActionResult> Index()
        {
            //Setup a user in the session.
            //TODO: Move this to login and retrieve from the db.
            //Session.SetUser(new User()
            //{
            //    Firstname = "Ally",
            //    Surname = "Murray",
            //    Usernmae = "15001325",
            //    Password = "3593"
            //});
            /////////////////////////////////

            //var user = Session.GetUser();

            //var user = await DocumentDbRepository<User>.GetItemAsync(id);
            //if (user == null) return HttpNotFound();

            var activitites = (await DocumentDbRepository<Activity>.GetAllItemsAsync()).ToList();

            var vm = new BookingViewModel();

            activitites.ForEach(x =>
            {
                vm.UserActivities.Add(new UserActivity()
                {
                    Id = x.Id,
                    Text = x.Text,
                    IsChecked = false
                });
            });

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBookings(string[] selectedActivityIds)
        {
            //Get the user out the session and update the array of button ids in the db.
            var user = await DocumentDbRepository<User>.GetItemAsync("1");
            user.ActivitiesIds = selectedActivityIds;

            await DocumentDbRepository<User>.UpdateItemAsync("1", user);


            return null;
        }

    }
}