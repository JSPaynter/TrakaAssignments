using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemoUI.Pages
{
    public class CheckoutPage
    {
        private readonly string btnCheckout = "checkout";
        private readonly string txtbxFirstName = "first-name";
        private readonly string txtbxLastName = "last-name";
        private readonly string txtbxZip = "postal-code";
        private readonly string btnContinue = "continue";
        private readonly string btnFinish = "finish";
        private readonly string txtCheckoutConfirmation = "checkout_complete_container";
        private readonly string cartItem = "cart_item";
        private readonly string txtProductTitle = "title_link";
        private readonly string btnRemove = "remove";

        protected IWebElement GetButtonCheckoutElement() => SauceDemoAPI.Driver.FindElement(By.Id(btnCheckout));
        protected IWebElement GetTextBoxFirstNameElement() => SauceDemoAPI.Driver.FindElement(By.Id(txtbxFirstName));
        protected IWebElement GetTextBoxLastNameElement() => SauceDemoAPI.Driver.FindElement(By.Id(txtbxLastName));
        protected IWebElement GetTextBoxZipElement() => SauceDemoAPI.Driver.FindElement(By.Id(txtbxZip));
        protected IWebElement GetButtonContinueElement() => SauceDemoAPI.Driver.FindElement(By.Id(btnContinue));
        protected IWebElement GetButtonFinishElement() => SauceDemoAPI.Driver.FindElement(By.Id(btnFinish));
        protected IWebElement GetTextConfirmationElement() => SauceDemoAPI.Driver.FindElement(By.Id(txtCheckoutConfirmation));
        protected List<IWebElement> GetCartItemElements() => [.. SauceDemoAPI.Driver.FindElements(By.ClassName(cartItem))];

        protected IWebElement GetCartElement(string productName)
        {
            var allProductElements = GetCartItemElements();
            var product = allProductElements.Where(x => x.FindElement(By.CssSelector($"[id$={txtProductTitle}]")).Text == productName).FirstOrDefault();
            return product ?? throw new Exception("Could not locate product " + productName);
        }

        public bool AmIOnBasket() => SauceDemoAPI.Driver.Url == "https://www.saucedemo.com/cart.html";

        public void RemoveProductFromBasket(string productName)
        {
            var productElement = GetCartElement(productName);
            productElement.FindElement(By.CssSelector($"[id^={btnRemove}]")).Click();
        }

        public void FillInInformation(string firstName, string lastName, string zip)
        {
            GetTextBoxFirstNameElement().SendKeys(firstName);
            GetTextBoxLastNameElement().SendKeys(lastName);
            GetTextBoxZipElement().SendKeys(zip);
        }

        public void PressCheckout() => GetButtonCheckoutElement().Click();
        public void PressContinue() => GetButtonContinueElement().Click();
        public void PressFinish() => GetButtonFinishElement().Click();

        public string GetCheckoutConfirmationText()
        {
            return GetTextConfirmationElement().Text;
        }

        public List<string> GetAllProductsInBasket()
        {
            return GetCartItemElements().Select(x => x.FindElement(By.CssSelector($"[id$={txtProductTitle}]")).Text).ToList();
        }
    }
}
