using System.Web;
using System.Web.SessionState;
using AutoBooking.Models.Models;

namespace AutoBooking.WebUI.Utilities
{
    public static class HttpSessionStateBaseExtensions
    {

        public static void SetUser(this HttpSessionStateBase session, ApplicationUser user)
        {
            session["User"] = user;
        }

        public static ApplicationUser GetUser(this HttpSessionStateBase session)
        {
            return session["User"] as ApplicationUser;
        }
    }

    public static class HttpSessionStateExtensions
    {

        public static void SetUser(this HttpSessionState session, ApplicationUser user)
        {
            session["User"] = user;
        }

        public static ApplicationUser GetUser(this HttpSessionState session)
        {
            return session["User"] as ApplicationUser;
        }
    }
}
