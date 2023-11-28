using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NaukariUtitlity.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace NaukariUtitlity.PageObjects
{
    class Naukari_Objects : SeleniumHelper
    {
        private IWebDriver driver;
        private WebDriverWait wait;


        public Naukari_Objects(IWebDriver driver)
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
        [FindsBy(How = How.Id, Using = "login_Layer")]
        [CacheLookup]
        private IWebElement login_button;

        [FindsBy(How = How.CssSelector, Using = "[placeholder='Enter your active Email ID / Username']")]
        [CacheLookup]
        private IWebElement username;

        [FindsBy(How = How.CssSelector, Using = "[placeholder='Enter your password']")]
        [CacheLookup]
        private IWebElement password;

        [FindsBy(How = How.CssSelector, Using = "[class='btn-primary loginButton']")]
        [CacheLookup]
        private IWebElement Login;
        


        #region
        /*------------------------------------------------*/
        //Public Methods - Reusable for Login Action
        /*------------------------------------------------*/
        #endregion

        public void ClickOnLogin()
        {
            login_button.Click();
            Thread.Sleep(2000);
            Console.WriteLine("Clicked on Login button..");
        }

        public void SignIn(string user,string pass)
        {
            username.Clear();
            username.SendKeys(user);
            Console.WriteLine("Entered username as Email id" + user);
            Thread.Sleep(50);
            password.Clear();
            password.SendKeys(pass);
            Thread.Sleep(50);
            Console.WriteLine("Entered password..");
            
        }
        public void ClickLogin() 
        { 
            Login.Click();
            Thread.Sleep(1500);
            Console.WriteLine("Logged in..");
        }


    }
}
