using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemoUI.Tests.OrderManagement
{
    [TestFixture]
    internal class PlaceOrder : SetupTeardown
    {
        [SetUp]
        public void SetUp()
        {
            steps.WhenTheUserLogsIn("standard_user", "secret_sauce");
        }

        [TestCase("Sauce Labs Backpack")]
        [TestCase("Sauce Labs Bike Light")]
        [TestCase("Sauce Labs Bolt T-Shirt")]
        [TestCase("Sauce Labs Fleece Jacket")]
        [TestCase("Sauce Labs Onesie")]
        [TestCase("Test.allTheThings() T-Shirt (Red)")]
        public void PlaceAnOrderSingleItem(string productName)
        {
            steps.WhenProductsAreAddedToTheBasket(productName);
            steps.WhenTheBasketIsCheckedOut();
            steps.ThenTheOrderShouldBePlaced();
        }

        // There is a bug where you cant have a string array as the first parameter
        [TestCase("", new string[] { "Sauce Labs Backpack", "Sauce Labs Bike Light" })]
        [TestCase("", new string[] { "Sauce Labs Backpack", "Sauce Labs Bike Light",
            "Sauce Labs Bolt T-Shirt", "Sauce Labs Fleece Jacket", "Sauce Labs Onesie",
            "Test.allTheThings() T-Shirt (Red)" })]
        public void PlaceAnOrderMultipleItems(string fix, string[] products)
        {
            steps.WhenProductsAreAddedToTheBasket(products);
            steps.WhenTheBasketIsCheckedOut();
            steps.ThenTheOrderShouldBePlaced();
        }
    }
}
