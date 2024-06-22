using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SauceDemoUI.Pages
{
    public class LoginPage : SauceDemoBase
    {
        private readonly string txtbxUsername = "user-name";
        private readonly string txtbxPassword = "password";
        private readonly string btnLogin = "login-button";
        private readonly string txtError = "/html/body/div/div/div[2]/div[1]/div/div/form/div[3]/h3";
        private readonly string loginContainer = "/html/body/div/div/div[1]";

        protected IWebElement GetTextBoxUserNameElement() => SauceDemoAPI.Driver.FindElement(By.Id(txtbxUsername));
        protected IWebElement GetTextBoxPasswordElement() => SauceDemoAPI.Driver.FindElement(By.Id(txtbxPassword));
        protected IWebElement GetButtonLoginElement() => SauceDemoAPI.Driver.FindElement(By.Id(btnLogin));
        protected IWebElement GetTextErrorElement() => SauceDemoAPI.Driver.FindElement(By.XPath(txtError));
        protected IWebElement GetLoginLogoElement() => SauceDemoAPI.Driver.FindElement(By.XPath(loginContainer));

        public bool AmIOnPage() => SauceDemoAPI.Driver.Url == "https://www.saucedemo.com/";

        public void WriteUsername(string username) => GetTextBoxUserNameElement().SendKeys(username);
        public void WritePassword(string password) => GetTextBoxPasswordElement().SendKeys(password);
        public void PressLoginButton() => GetButtonLoginElement().Click();

        public void Login(string username, string password)
        {
            WriteUsername(username);
            WritePassword(password);
            PressLoginButton();
        }

        public string GetErrorMessage()
        {
            return GetTextErrorElement().Text;
        }
    }
}
