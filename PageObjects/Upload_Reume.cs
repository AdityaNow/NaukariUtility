using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace NaukariUtitlity.PageObjects
{
    class Upload_Resume : SeleniumHelper
    {
        private IWebDriver driver;
        private WebDriverWait wait;
       

        public Upload_Resume(IWebDriver driver)
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
        [FindsBy(How = How.XPath, Using = "//input[@value='Update resume' and @type='button']")]
        [CacheLookup]
        private IWebElement uploadResumeBtn;

        [FindsBy(How = How.Id, Using = "attachCV")]
        [CacheLookup]
        private IWebElement uploadResumeInput;

        [FindsBy(How = How.XPath, Using = "//div[@class='msgBox success ']")]
        [CacheLookup]
        private IWebElement successMessageBox;



        #region
        /*------------------------------------------------*/
        //Public Methods - Reusable for Login Action
        /*------------------------------------------------*/
        #endregion

        public void UploadResume(string filePath)
        {
            if (uploadResumeBtn.Displayed)
            {
                if (uploadResumeInput.Enabled) 
                {
                    uploadResumeInput.SendKeys(filePath);
                }
            }
            Thread.Sleep(100);
            DefaultWait<IWebElement> fluentWait = new DefaultWait<IWebElement>(successMessageBox);;
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(10);
            fluentWait.IgnoreExceptionTypes(typeof(TimeoutException), typeof(NullReferenceException), typeof(StaleElementReferenceException));
            if (successMessageBox.Displayed) {
                successMessageBox.FindElement(By.XPath("//p[@class='head' and contains(text(),'Success')]"));
                successMessageBox.FindElement(By.XPath("//p[@class='msg' and contains(text(),'Resume has been successfully uploaded.')]"));
            }
            else
            {
                Assert.Fail("Unable to find Success Message Upon Resume Upload");
            }
        }      

    }
}
