using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemoUI.Tests
{
    [TestFixture]
    internal class LoginTesting : SetupTeardown
    {
        [TestCase("standard_user", "secret_sauce")]
        [TestCase("visual_user", "secret_sauce")]
        public void TestUsersCanLogin(string user, string password)
        {
            steps.GivenAUserExists(user);
            steps.WhenTheUserLogsIn(user, password);
            steps.ThenTheUserShouldBeOnTheProductsScreen();
        }

        [TestCase("locked_out_user", "secret_sauce", "Epic sadface: Sorry, this user has been locked out.")]
        public void TestUserIsLockedOut(string user, string password, string expectedErrorMessage)
        {
            steps.GivenAUserExists("Locked User");
            steps.WhenTheUserLogsIn(user, password);
            steps.ThenALoginErrorMessageShouldBeDisplayed(expectedErrorMessage);
            steps.ThenTheUserShouldBeOnTheLoginScreen();
        }
    }
}
