using DataAccess.DataBaseLayer;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace DataAccess.SeleniumHtmlParse
{
    public class CurrencyParserRepository : ICurrencyParserRepository
    {
        public IEnumerable<Currency> GetCurrencies(string selector, string url)
        {
            using (var parseData = new GenericRepository(url))
            {
                var dataWebElements = parseData.GetDataList(By.XPath(selector));
                 
                var resultCurrencies = new List<Currency>();

                for (int i = 1; i < dataWebElements.Count; i++)
                {
                    var text = dataWebElements[i].Text;
                    var nameLat = text.Split(' ')[0];
                    var logoAndNameRus = dataWebElements[i].Text.TrimStart($"{nameLat} ".ToCharArray()).Split(" - ");

                    string logo = logoAndNameRus[0];
                    string nameRus = logoAndNameRus[1];

                    resultCurrencies.Add(new Currency
                    {
                        NameLat = nameLat,
                        NameRus = nameRus,
                        Logo = logo,
                        Url = dataWebElements[i].GetAttribute("value")
                    });
                }
                return resultCurrencies;
            }
        }
    }
}
