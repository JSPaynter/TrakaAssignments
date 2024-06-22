using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities;

namespace SauceDemoUI
{
    public class Steps
    {
        private SauceDemoAPI sauceDemo;

        public Steps(SauceDemoAPI sauceDemoApi)
        {
            this.sauceDemo = sauceDemoApi;
        }

        public void TearDownSauceDemo()
        {
            SauceDemoAPI.Driver.Quit();
        }

        public void GivenAUserExists(string description)
        {
            /* Users are alrady created on demo site:
             * standard_user
             * locked_out_user
             * problem_user
             * performance_glitch_user
             * error_user
             * visual_user
             */
        }

        internal void WhenTheUserLogsIn(string user, string password)
        {
            sauceDemo.Login(user, password);
            sauceDemo.Screenshot("LoginScreen");
        }

        internal void WhenProductsAreAddedToTheBasket(string productName)
        {
            sauceDemo.GoToProductsPage();
            sauceDemo.AddProductToBasket(productName);
            sauceDemo.Screenshot("ProductScreenProductAdded - " + productName);
        }

        internal void WhenProductsAreAddedToTheBasket(string[] products)
        {
            products.ToList().ForEach(WhenProductsAreAddedToTheBasket);
        }

        internal void WhenProductsAreRemovedFromTheBasket(string productToRemove)
        {
            sauceDemo.GoToBasket();
            sauceDemo.RemoveProductFromBasket(productToRemove);
            sauceDemo.Screenshot("BasketScreenProductRemoved - " + productToRemove);
        }

        internal void WhenTheBasketIsCheckedOut()
        {
            WhenTheBasketIsCheckedOut("FirstName", "LastName", "Zip");
        }

        internal void WhenTheBasketIsCheckedOut(string firstName, string lastName, string zip)
        {
            sauceDemo.CheckoutBasket(firstName, lastName, zip);
            sauceDemo.Screenshot("BasketCheckedOut");
        }

        internal void ThenTheUserShouldBeOnTheLoginScreen()
        {
            sauceDemo.Screenshot("ThenTheUserShouldBeOnTheLoginScreen");
            Assert.That(sauceDemo.LoginPage.AmIOnPage(), "User is no longer on login page");
        }

        internal void ThenALoginErrorMessageShouldBeDisplayed(string expectedErrorMessage)
        {
            sauceDemo.Screenshot("ThenALoginErrorMessageShouldBeDisplayed");
            string errorMessage = sauceDemo.LoginPage.GetErrorMessage();
            Assert.That(errorMessage, Is.EqualTo(expectedErrorMessage));
        }

        internal void ThenTheOrderShouldBePlaced()
        {
            sauceDemo.Screenshot("ThenTheOrderShouldBePlaced");
            Assert.That(sauceDemo.GetCheckoutCompleteText(),
                Is.EqualTo("Thank you for your order!\r\nYour order has been dispatched, and will arrive just as fast as the pony can get there!\r\nBack Home"));
        }

        internal void ThenTheBasketShouldContainTheCorrectProducts(string[] strings)
        {
            sauceDemo.GoToBasket();
            sauceDemo.Screenshot("ThenTheBasketShouldContainTheCorrectProducts");
            Assert.That(strings.ToList(), Is.EquivalentTo(sauceDemo.GetBasketItemNames()));
        }

        internal void ThenTheUserShouldBeOnTheProductsScreen()
        {
            sauceDemo.Screenshot("ThenTheUserShouldBeOnTheProductsScreen");
            Assert.That(sauceDemo.ProductsPage.AmIOnPage(), "User is not on products page");
        }
    }
}
