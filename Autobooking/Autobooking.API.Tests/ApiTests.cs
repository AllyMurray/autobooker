using System;
using System.Threading.Tasks;
using AutoBooking.API;
using AutoBooking.Models;
using AutoBooking.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Autobooking.API.Tests
{
    [TestClass]
    public class ApiTests
    {
        private static ApplicationUser user;

        [ClassInitialize]
        public static async Task TestClass(TestContext testContext)
        {
            DocumentDbRepository<ApplicationUser>.Initialize();
            user = await DocumentDbRepository<ApplicationUser>.GetFirstItemAsync();
        }

        [TestMethod]
        public void Login_ValidCredentials_ReturnsTrue()
        {
            //Arrange
            var api = new Api(user.LeisureBookingUsername, user.LeisureBookingPassword);
            api.LogOut();

            //Act
            var result = api.Login();

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Login_InvalidCredentials_ReturnsFalse()
        {
            //Arrange
            var api = new Api("invalidUsername", "invalidPassword");

            //Act
            var result = api.Login();

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetActivities_LoggedIn_ReturnsActivitiesCollection()
        {
            //Arrange
            var api = new Api(user.LeisureBookingUsername, user.LeisureBookingPassword);

            //Act
            var result = api.GetActivities();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetActivities_NotLoggedIn_ThrowsException()
        {
            //Arrange
            var api = new Api(user.LeisureBookingUsername, user.LeisureBookingPassword);

            //Act
            var result = api.GetActivities();

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
