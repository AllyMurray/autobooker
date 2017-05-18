using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoBooking.Api;
using AutoBooking.Models;
using AutoBooking.Models.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace AutoBooking.API
{
    public class Api
    {
        // TODO: Move urls to the database.
        private const string BaseUrl = "https://leisurebookings.aberdeenshire.gov.uk/";
        private const string LoginUrl = "Connect/mrmlogin.aspx?Culture=en-GB";
        private const string SelectActivityTypeUrl = "Connect/mrmselectsite.aspx?disableSiteSelection=1";

        private static readonly PhantomJSDriver PhantomJsDriver = new PhantomJSDriver();


        private string _username;
        private string _password;

        public Api(string username, string password)
        {
            _username = username;
            _password = password;

            Login();
        }

        public bool Login()
        {
            PhantomJsDriver.Navigate().GoToUrl($"{BaseUrl}{LoginUrl}");
            PhantomJsDriver.FindElementById("ctl00_MainContent_InputLogin").SendKeys(_username);
            PhantomJsDriver.FindElementById("ctl00_MainContent_InputPassword").SendKeys(_password);
            PhantomJsDriver.FindElementById("ctl00_MainContent_btnLogin").Click();
            PhantomJsDriver.WaitForAjax();

            return PhantomJsDriver.Url != $"{BaseUrl}{LoginUrl}";
        }

        public IEnumerable<Activity> GetActivities()
        {
            NavigateToSelectActivity();

            var webElements = PhantomJsDriver.FindElementsByXPath("//input[contains(@name,'MainContent$activitiesGrid')]");

            var activities = webElements.Select(x => new Activity()
            {
                Id = x.GetAttribute("id"),
                Text = x.GetAttribute("value")
            });

            return activities;
        }

        public void BookActivity(string activityButtonId)
        {
            NavigateToSelectActivity();

            // Now select a class to book onto using the button id passed in.
            PhantomJsDriver.FindElementById(activityButtonId).Click();
            PhantomJsDriver.WaitForAjax();

            // Click the button to go to the booking screen.
            // TODO: Hnadle scenarios where classes are full/unavailable.
            PhantomJsDriver.FindElementById("ctl00_MainContent_ClassStatus_ctrl0_btnBook").Click();
            PhantomJsDriver.WaitForAjax();

            // Click the button to book the desired class.
            PhantomJsDriver.FindElementById("ctl00_MainContent_btnBasket").Click();
            PhantomJsDriver.WaitForAjax();
        }

        public void CancelActivity()
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            if(PhantomJsDriver.Url != $"{BaseUrl}{LoginUrl}")
                PhantomJsDriver.FindElementById("ctl00_LoginControl_Logoutlnk").Click();
        }

        public void ChangeUser(string username, string password)
        {
            _username = username;
            _password = password;

            LogOut();
            Login();
        }


        private static void NavigateToSelectActivity()
        {
            // We must navigate to the select activity url and click the button, 
            // this fires the javascipt required to properly load the page with the adult sports activites.
            PhantomJsDriver.Navigate().GoToUrl($"{BaseUrl}{SelectActivityTypeUrl}");

            if (PhantomJsDriver.Url == $"{BaseUrl}{LoginUrl}")
                throw new Exception("You are not logged into the website.");

            PhantomJsDriver.FindElementById("ctl00_MainContent_activityGroupsGrid_ctrl0_lnkListCommand").Click();
            PhantomJsDriver.WaitForAjax();
        }
    }
}
