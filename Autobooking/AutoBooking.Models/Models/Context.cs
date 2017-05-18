using System.Data.Entity;

namespace AutoBooking.Models.Models
{


    public class Context : DbContext
    {
        public Context() : base("name=Context")
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<WhiteListedEmail> WhiteListedEmails { get; set; }
    }
}
