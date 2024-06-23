using Core.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SauceDemoUI.Environment;

namespace SauceDemoUI.Tests
{
    public class SetupTeardown
    {
        internal Steps steps;
        bool oneTimeSetupRan = false;

        [OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
            if (!oneTimeSetupRan)
            {
                if (!Directory.Exists(EnvironmentSettings.ScreenshotFolderName))
                    Directory.CreateDirectory(EnvironmentSettings.ScreenshotFolderName);
                oneTimeSetupRan = true;
            }
        }

        [SetUp]
        public void BaseSetUp()
        {
            string screenShotTestFolder = Path.Combine(EnvironmentSettings.ScreenshotFolderName, TestContext.CurrentContext.Test.MethodName);
            if (!Directory.Exists(screenShotTestFolder))
                Directory.CreateDirectory(screenShotTestFolder);

            var options = new ChromeOptions
            {
                ImplicitWaitTimeout = new TimeSpan(0, 0, 3)
            };
#if RELEASE
            options.AddArgument("headless");
#endif
            var driver = new ChromeDriver(options);
            SauceDemoAPI sauceDemoApi = new(driver);
            steps = new Steps(sauceDemoApi);
        }

        [TearDown]
        public void BaseTearDown()
        {
            steps.TearDownSauceDemo();
        }
    }
}
