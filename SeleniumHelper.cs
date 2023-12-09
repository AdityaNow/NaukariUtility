using System;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace NaukariUtitlity
{
    public class SeleniumHelper
    {
        public static String SetBrowserFromEnvVar(IConfigurationRoot config) 
        {
            string Browser = string.Empty;
            string Br = string.Empty;
            Browser = config["browser"];
            Console.WriteLine("Current Browser ="+Browser);
            Console.WriteLine("Getting environmental variable for browser..");
            Br = Environment.GetEnvironmentVariable("BrowserEnv");
            if (!string.IsNullOrEmpty(Br)) 
            {
                Browser = Br;
                Console.WriteLine("Current Browser from Env Variable "+Browser);
            }
            return Browser;
        }

        public static String SetPassFromEnvVar(IConfigurationRoot config)
        {
            string Pass = string.Empty;
            string Ps = string.Empty;
            Pass = config["password2"];
            Console.WriteLine("Current Browser =" + Pass);
            Console.WriteLine("Getting environmental variable for browser..");
            Ps = Environment.GetEnvironmentVariable("PassEnv");
            if (!string.IsNullOrEmpty(Ps))
            {
                Pass = Ps;
                Console.WriteLine("Current Browser from Env Variable " + Pass);
            }
            return Pass;
        }

        public static IWebElement WaitUntilElementExists(IWebDriver driver, By elementLocator, int timeout = 15)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementExists(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        public static IWebElement WaitUntilElementVisible(IWebDriver driver, By elementLocator, int timeout = 15)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
                throw;
            }
        }

        public static IWebElement WaitUntilElementClickable(IWebDriver driver, By elementLocator, int timeout = 15)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        public static void WaitForPageToLoad(IWebDriver driver, int timeout = 180)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                IWebElement oldPage = driver.FindElement(By.TagName("html"));
                wait.Until(driver => ExpectedConditions.StalenessOf(oldPage)(driver) &&
                ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Page has not loaded yet..");
                throw;
            }
        }

    }
}
