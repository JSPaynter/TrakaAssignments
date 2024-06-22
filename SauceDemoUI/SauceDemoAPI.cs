using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SauceDemoUI.Pages;
using System.Net;
using NUnit.Framework;
using Core.Utilities;

namespace SauceDemoUI
{
    public class SauceDemoAPI
    {
        public static IWebDriver Driver;
        private ScreenshotHelper ScreenshotHelper;

        public LoginPage LoginPage = new();
        public ProductsPage ProductsPage = new();
        public CheckoutPage Checkout = new();

        public SauceDemoAPI(IWebDriver webDriver)
        {
            Driver = webDriver;
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            ScreenshotHelper = new ScreenshotHelper(Driver);
        }

        public void Screenshot(string name)
        {
            ScreenshotHelper.TakeScreenShot(TestContext.CurrentContext.Test.MethodName + "/" + name);
        }

        public void Login(string username, string password)
        {
            LoginPage.Login(username, password);
        }

        public void AddProductToBasket(string productName)
        {
            ProductsPage.AddProductToBasket(productName);
        }

        public void GoToProductsPage()
        {
            if (!ProductsPage.AmIOnPage())
                ProductsPage.GoToProductsPage();
        }

        public void GoToBasket()
        {
            if (!Checkout.AmIOnBasket())
                ProductsPage.PressCart();
        }

        public void RemoveProductFromBasket(string productName)
        {
            Checkout.RemoveProductFromBasket(productName);
        }

        public void CheckoutBasket(string firstName, string lastName, string zip)
        {
            GoToBasket();
            Checkout.PressCheckout();
            Checkout.FillInInformation(firstName, lastName, zip);
            Checkout.PressContinue();
            Checkout.PressFinish();
        }

        public string GetCheckoutCompleteText()
        {
            return Checkout.GetCheckoutConfirmationText();
        }

        public List<string> GetBasketItemNames()
        {
            return Checkout.GetAllProductsInBasket();
        }
    }
}
