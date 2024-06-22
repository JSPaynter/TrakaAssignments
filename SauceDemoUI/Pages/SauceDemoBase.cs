using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SauceDemoUI.Pages
{
    public class SauceDemoBase
    {
        private readonly string btnCart = "shopping_cart_container";
        private readonly string btnMenu = "react-burger-menu-btn";
        private readonly string btnAllItems = "inventory_sidebar_link";

        protected IWebElement GetButtonCart() => SauceDemoAPI.Driver.FindElement(By.Id(btnCart));
        protected IWebElement GetButtonMenu() => SauceDemoAPI.Driver.FindElement(By.Id(btnMenu));
        protected IWebElement GetButtonAllItems () => SauceDemoAPI.Driver.FindElement(By.Id(btnAllItems));

        public void PressCart() => GetButtonCart().Click();
        public void PressMenu() => GetButtonMenu().Click();
        public void PressAllItems() => GetButtonAllItems().Click();

        public void GoToProductsPage()
        {
            PressMenu();
            Thread.Sleep(1000);
            PressAllItems();
        }
    }
}
