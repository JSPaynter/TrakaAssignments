using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Utilities
{
    public class ScreenshotHelper(IWebDriver driver)
    {
        private readonly IWebDriver driver = driver;
        public readonly static string screenshotFolderName = "./../../../../Screenshots/";

        public void TakeScreenShot(string fileName)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(screenshotFolderName + fileName + ".png");
        }
    }
}
