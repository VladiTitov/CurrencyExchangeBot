using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DataAccess.DataBaseLayer;
using OpenQA.Selenium;

namespace DataAccess.SeleniumHtmlParse
{
    public class BaseParserRepository : IBaseParserRepository
    {
        public IEnumerable<BaseEntity> GetData(string selector, string url)
        {
            using (var parseData = new GenericRepository(url))
            {
                List<BaseEntity> result = new List<BaseEntity>();

                var buttons = parseData.GetDataList(By.ClassName("expand"));
                foreach (var btn in buttons)
                {
                    btn.Click();
                    Thread.Sleep(100);
                }
                var data = parseData.GetDataList(By.XPath(selector));
                var dropData = DropData(data);
                return dropData.Select(ParseData).ToList();
            }
        }

        private static BaseEntity ParseData(List<IWebElement> elements)
        {
            string[] nameAndAdr = elements[0].FindElement(By.ClassName("btn-tomap")).GetAttribute("data-name").Split(": ");
            string phones = elements[0].FindElement(By.ClassName("phones")).Text;
            string bankName = nameAndAdr[0];
            string adr = nameAndAdr[1];
            string bestBuy = elements[1].Text;
            string bestSale = elements[2].Text;

            return new BaseEntity
            {
                BankName = bankName,
                Adr = adr,
                Buy = bestBuy,
                Sale = bestSale,
                Phone = phones
            };
        }

        private static IEnumerable<List<IWebElement>> DropData(IReadOnlyCollection<IWebElement> data)
        {
            return Enumerable.Range(0, data.Count / 3)
                .Select(i => data.Skip(i * 3)
                    .Take(3)
                    .ToList())
                .ToList();
        }
    }
}
