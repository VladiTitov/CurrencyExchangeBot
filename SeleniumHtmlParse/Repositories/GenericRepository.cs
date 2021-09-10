using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace DataAccess.SeleniumHtmlParse
{
    public class GenericRepository : IGenericRepository, IDisposable
    {
        private readonly RemoteWebDriver _driver;

        public GenericRepository(string url)
        {
            string remoteUrlChrome = "http://10.5.0.3:4444/wd/hub";
            ChromeOptions chromeOptions = new ChromeOptions();
            _driver = new RemoteWebDriver(new Uri(remoteUrlChrome), chromeOptions);
            _driver.Navigate().GoToUrl(url);
        }

        public void Dispose() 
        {
            _driver.Close();
            _driver.Quit();
        }

        public string GetString() => _driver.PageSource;


        public IWebElement GetData(By selector) => _driver.FindElement(selector);

        public IReadOnlyList<IWebElement> GetDataList(By selector) => _driver.FindElements(selector);
    }
}
