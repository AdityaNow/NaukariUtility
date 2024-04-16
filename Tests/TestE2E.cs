using Microsoft.Extensions.Configuration;
using NaukariUtitlity.PageObjects;
using NaukariUtitlity.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using WebDriverManager.DriverConfigs.Impl;

namespace NaukariUtitlity
{
    public class TestE2E : SeleniumHelper
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string emailID = string.Empty;
        private string password = string.Empty;
        private string ActualHeadline = "";
        private string ExperimentalHeadline = "";
        private string resumeUpload = "";
        private string url = null;
        private string browser = null;

        //[SetUp]
        [OneTimeSetUp]
        public void Setup()
        {
            var settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var testDt = new ConfigurationBuilder().AddJsonFile("TestData\\testData.json").Build();
            //this.browser = settings["browser"];
            //this.browser = System.Environment.GetEnvironmentVariable("browser");
            this.browser = SetBrowserFromEnvVar(settings);
            this.url = settings["url"];
            string p1 = settings["pass1"];
            //string p2 = testDt["password2"];
            string p2 = SetPassFromEnvVar(testDt);
            this.password = string.Concat(p1, p2);
            Console.WriteLine("Launched" + password);
            BrowserSelector br = new BrowserSelector();
            driver = br.FindDriver(browser,url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);

            Console.WriteLine("Launched"+url);
            this.emailID = settings["mail"];
            this.ActualHeadline = settings["ResumeHeadline"];
            this.ExperimentalHeadline = settings["RH1"];
            this.resumeUpload = testDt["ResumeUploadFile"];
        }
        //------------------------------------------------------------------------
        //------------------------------------------------------------------------
        [Test, Order(1),Category("smoke")]
        public void VerifyTheLandingPage()
        {
            Thread.Sleep(10000);
            string title = driver.Title; //Fetch Title
            Assert.AreEqual(title, "Jobs - Recruitment - Job Search - Employment - Job Vacancies - Naukri.com");
            Console.WriteLine("Landing Title Verified");
        }
        //------------------------------------------------------------------------
        [Test, Order(2), Category("smoke")]
        public void LoginToNaukari()
        {
            Naukari_Objects no = new Naukari_Objects(driver);
           
            no.ClickOnLogin();
            Thread.Sleep(1000);
            no.SignIn(emailID,password);
            no.ClickLogin();
            
        }
        //------------------------------------------------------------------------
        [Test, Order(3), Category("smoke")]
        public void UpdateProfile() 
        {
            MiscellaneousUtility msc = new MiscellaneousUtility(driver);
            Edit_Profile ep = new Edit_Profile(driver);

            msc.HoverOverMyNaukariTab();
            msc.ClickUpdateProfile();
            Console.WriteLine("Clicked and Landed on Update Profile page..");
            ep.EditResumeHeadline();
            ep.updateResumeHeadline(ExperimentalHeadline);
            Thread.Sleep(1500);
            ep.EditResumeHeadline();
            ep.updateResumeHeadline(ActualHeadline);
            Console.WriteLine("done");
        }

        [Test, Order(4), Category("regression")]
        public void UploadResume()
        {
            MiscellaneousUtility msc = new MiscellaneousUtility(driver);
            Edit_Profile ep = new Edit_Profile(driver);
            Upload_Resume up = new Upload_Resume(driver);
            #region - Uncomment if you want to run this test as a standalone test
            //LoginToNaukari();
            //msc.HoverOverMyNaukariTab();
            //msc.ClickUpdateProfile();
            #endregion
            Console.WriteLine("Clicked and Landed on Update Profile page..");
            up.UploadResume(resumeUpload);
            Console.WriteLine("done");
        }

        //[TearDown]
        [OneTimeTearDown]
        public void Cleanup()
        {
            driver.Close();
            if (driver != null)
            {
              driver.Quit();
            }
        }
    }
}

