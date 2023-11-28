using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace NaukariUtitlity.Utilities
{
    class MiscellaneousUtility : SeleniumHelper
    {
        //Global Webdriver and Wait instance
        private IWebDriver driver;
        private WebDriverWait wait;

        public MiscellaneousUtility(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        #region
        /*------------------------------------------------*/
        //Private WebElements-----------------------------//
        /*------------------------------------------------*/
        #endregion
        [FindsBy(How = How.XPath, Using = "//div[@class='nI-gNb-drawer__icon']")]
        [CacheLookup]
        private IWebElement MyNaukariProfileDrawer;

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'View & Update Profile')]")]
        [CacheLookup]
        private IWebElement UpdateProfile;




        public void HoverOverMyNaukariTab() 
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            Actions action = new Actions(driver);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            Thread.Sleep(500);
            MyNaukariProfileDrawer.Click();
            //action.MoveToElement(MyNaukariProfileDrawer).ClickAndHold().Build().Perform();
            Thread.Sleep(100);
            Console.WriteLine("Hover over My Naukari Tab to click on Edit Profile");
        }

        public void ClickUpdateProfile() => UpdateProfile.Click();

        public void switchToTab(int i)
        {
            driver.SwitchTo().Window(driver.WindowHandles[i]);
        }

        

    }
}
