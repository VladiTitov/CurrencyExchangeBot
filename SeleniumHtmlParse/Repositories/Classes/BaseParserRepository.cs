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
                var selectedDataList = parseData.GetDataList(By.XPath(selector));
                var dropSelectedDataList = DropData(selectedDataList);
                return dropSelectedDataList.Select(ParseData).ToList();
            }
        }

        private BaseEntity ParseData(List<IWebElement> elements)
        {
            string[] nameAndAdr = elements[0].FindElement(By.ClassName("btn-tomap")).GetAttribute("data-name").Split(": ");
            string[] phones = elements[0].FindElement(By.ClassName("phones")).Text.Split("\r\n");
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

        private List<List<IWebElement>> DropData(IReadOnlyCollection<IWebElement> data)
        {
            return Enumerable.Range(0, data.Count / 3)
                .Select(i => data.Skip(i * 3)
                    .Take(3)
                    .ToList())
                .ToList();
        }
    }
}
