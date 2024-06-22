using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SauceDemoUI.Pages
{
    public class ProductsPage : SauceDemoBase
    {
        private readonly string inventoryItemName = "inventory_item";
        private readonly string btnAddToCart = "add-to-cart";
        private readonly string txtProductTitle = "title_link";

        protected List<IWebElement> GetAllProductElements() => [.. SauceDemoAPI.Driver.FindElements(By.ClassName(inventoryItemName))];
        protected IWebElement GetProductElement(string productName)
        {
            var allProductElements = GetAllProductElements();
            var product = allProductElements.Where(x => x.FindElement(By.CssSelector($"[id$={txtProductTitle}]")).Text == productName).FirstOrDefault();
            return product ?? throw new Exception("Could not locate product " + productName);
        }

        public void AddProductToBasket(string productName)
        {
            var productElement = GetProductElement(productName);
            productElement.FindElement(By.CssSelector($"[id^={btnAddToCart}]")).Click();
        }

        public bool AmIOnPage() => SauceDemoAPI.Driver.Url == "https://www.saucedemo.com/inventory.html";
    }
}
