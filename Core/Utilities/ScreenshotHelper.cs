using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Utilities
{
    public class ScreenshotHelper
    {
        private readonly IWebDriver Driver;
        public readonly string ScreenshotFolderName = "";

        public ScreenshotHelper(IWebDriver driver, string screenshotFolderName)
        {
            Driver = driver;
            ScreenshotFolderName = screenshotFolderName;
        }

        public void TakeScreenShot(string fileName)
        {
            Screenshot ss = ((ITakesScreenshot)Driver).GetScreenshot();
            ss.SaveAsFile(ScreenshotFolderName + fileName + ".png");
        }
    }
}
