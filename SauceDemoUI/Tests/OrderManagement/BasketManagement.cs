using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemoUI.Tests.OrderManagement
{
    [TestFixture]
    internal class BasketManagement : SetupTeardown
    {
        [SetUp]
        public void SetUp()
        {
            steps.WhenTheUserLogsIn("standard_user", "secret_sauce");
        }

        [TestCase("Sauce Labs Backpack", new string[] { "Sauce Labs Backpack", "Sauce Labs Bike Light" })]
        [TestCase("Sauce Labs Backpack", new string[] { "Sauce Labs Backpack" })]
        public void RemoveItemFromBasket(string productToRemove, string[] productsToAdd)
        {
            steps.WhenProductsAreAddedToTheBasket(productsToAdd);
            steps.WhenProductsAreRemovedFromTheBasket(productToRemove);
            steps.ThenTheBasketShouldContainTheCorrectProducts(productsToAdd.Where(x => x != productToRemove).ToArray());
        }

        [TestCase("Sauce Labs Backpack", new string[] { "Sauce Labs Backpack", "Sauce Labs Bike Light" })]
        public void ReAddItemsToBasketAfterRemoving(string productToRemove, string[] productsToAdd)
        {
            steps.WhenProductsAreAddedToTheBasket(productsToAdd);
            steps.WhenProductsAreRemovedFromTheBasket(productToRemove);
            steps.WhenProductsAreAddedToTheBasket(productToRemove);
            steps.ThenTheBasketShouldContainTheCorrectProducts(productsToAdd);
        }
    }
}
