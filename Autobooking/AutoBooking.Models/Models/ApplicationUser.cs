using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DocumentDB.AspNet.Identity;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using AutoBooking.Models;

namespace AutoBooking.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        private string _leisureBookingUsername;
        private string _leisureBookingPassword;

        public string LeisureBookingUsernameEncrypted
        {
            get => _leisureBookingUsername;
            set => _leisureBookingUsername =
                StringCipher.Encrypt(value, ConfigurationManager.AppSettings["passphrase"]);
        }

        [JsonIgnore]
        public string LeisureBookingUsername => StringCipher.Decrypt(_leisureBookingUsername, ConfigurationManager.AppSettings["passphrase"]);

        public string LeisureBookingPasswordEncrypted
        {
            get => _leisureBookingPassword;
            set => _leisureBookingPassword =
                StringCipher.Encrypt(value, ConfigurationManager.AppSettings["passphrase"]);
        }

        [JsonIgnore]
        public string LeisureBookingPassword => StringCipher.Decrypt(_leisureBookingPassword, ConfigurationManager.AppSettings["passphrase"]);

        public string[] ActivityIds { get; set; }

    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
