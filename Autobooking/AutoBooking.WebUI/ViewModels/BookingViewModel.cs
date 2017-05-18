using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoBooking.Models.Models;

namespace AutoBooking.WebUI.ViewModels
{
    public class UserActivity
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public bool IsChecked { get; set; }
    }

    public class BookingViewModel
    {
        public IList<UserActivity> UserActivities { get; set; }

        public BookingViewModel()
        {
            UserActivities = new List<UserActivity>();
        }
    }
}