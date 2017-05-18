using System.Web;
using System.Web.SessionState;
using AutoBooking.Models.Models;

namespace AutoBooking.WebUI.Utilities
{
    public static class HttpSessionStateBaseExtensions
    {

        public static void SetUser(this HttpSessionStateBase session, User user)
        {
            session["User"] = user;
        }

        public static User GetUser(this HttpSessionStateBase session)
        {
            return session["User"] as User;
        }
    }

    public static class HttpSessionStateExtensions
    {

        public static void SetUser(this HttpSessionState session, User user)
        {
            session["User"] = user;
        }

        public static User GetUser(this HttpSessionState session)
        {
            return session["User"] as User;
        }
    }
}
