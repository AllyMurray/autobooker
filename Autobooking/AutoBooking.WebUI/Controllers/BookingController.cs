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
    [RequireHttps]
    public class BookingController : Controller
    {
        [OutputCache(Duration = 86400, VaryByParam = "none")]
        // GET: Booking
        public async Task<ActionResult> Index()
        {
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
            var user = await DocumentDbRepository<ApplicationUser>.GetItemAsync("1");
            user.ActivityIds = selectedActivityIds;

            await DocumentDbRepository<ApplicationUser>.UpdateItemAsync("1", user);


            return null;
        }

    }
}