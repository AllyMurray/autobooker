using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoBooking.WebUI.Startup))]
namespace AutoBooking.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
