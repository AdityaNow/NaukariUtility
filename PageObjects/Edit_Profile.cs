using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Microsoft.Extensions.Configuration;
using NaukariUtitlity.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace NaukariUtitlity.PageObjects
{
    class Edit_Profile : SeleniumHelper
    {
        private IWebDriver driver;
        private WebDriverWait wait;
       


        public Edit_Profile(IWebDriver driver)
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
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Resume headline')]/following-sibling::span")]
        [CacheLookup]
        private IWebElement editResumeHeadline;

        



        #region
        /*------------------------------------------------*/
        //Public Methods - Reusable for Login Action
        /*------------------------------------------------*/
        #endregion

        public void EditResumeHeadline()
        {
            Thread.Sleep(500);
            editResumeHeadline.Click();
            Thread.Sleep(200);
            Console.WriteLine("Clicked on Pencil icon for Resume Headline button..");
        }

        public void updateResumeHeadline(string head)
        {
            string css = "[id='resumeHeadlineTxt']";   //Stale Element Exception
            IWebElement headline = driver.FindElement(By.CssSelector("[id='resumeHeadlineTxt']"));
            DefaultWait<IWebElement> wait = new DefaultWait<IWebElement>(headline);
            wait.Timeout = TimeSpan.FromMinutes(1);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);
            Thread.Sleep(2000);
            headline.Clear();
            Thread.Sleep(200);
            headline.SendKeys(head);
            Thread.Sleep(500);
            string save1 = "//button[@type='submit' and text()='Save']"; //Stale Element Exception
            IWebElement s1 = driver.FindElement(By.XPath(save1));
            s1.Click();
            Thread.Sleep(3000);
            Console.WriteLine("Updated Resume Headline..");
        }
      

    }
}
