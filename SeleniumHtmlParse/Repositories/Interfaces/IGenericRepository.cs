using System.Collections.Generic;
using OpenQA.Selenium;

namespace DataAccess.SeleniumHtmlParse
{
    public interface IGenericRepository
    {
        IWebElement GetData(By selector);
        IReadOnlyList<IWebElement> GetDataList(By selector);
    }
}
