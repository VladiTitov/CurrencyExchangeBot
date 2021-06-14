using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DataAccess.SeleniumHtmlParse
{
    class GenericRepository : IGenericRepository, IDisposable
    {
        private readonly IWebDriver _driver;

        public GenericRepository(string url) =>
            _driver = new ChromeDriver() { Url = url };

        public void Dispose() =>
            _driver.Close();

        public IWebElement GetData(By selector) =>
            _driver.FindElement(selector);

        public IReadOnlyList<IWebElement> GetDataList(By selector) =>
            _driver.FindElements(selector);
    }
}
