using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DataAccess.DataBaseLayer;
using OpenQA.Selenium;
using HtmlAgilityPack;
using System;
using BusinessLogic.Logger;

namespace DataAccess.SeleniumHtmlParse
{
    public class BaseParserRepository : IBaseParserRepository
    {
        public IEnumerable<BaseEntity> GetData(string selector, string url)
        {
            string pageString = "";
            using (var parseData = new GenericRepository(url))
            {
                var buttons = parseData.GetDataList(By.ClassName("expand"));
                foreach (var btn in buttons)
                {
                    try
                    {
                        btn?.Click();
                    }
                    catch (Exception ex)
                    {
                        LoggingService.AddEventToLog(ex.Message);
                    }
                    
                    Thread.Sleep(100);
                }

                pageString = parseData.GetString();
            }

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(pageString);

            var nodes = htmlDoc.DocumentNode.SelectNodes(".//*/tbody/tr/td/table/tbody/tr");

            return ParseData(nodes);
        }

        private List<BaseEntity> ParseData(HtmlNodeCollection nodes)
        {
            
            List<BaseEntity> objects = new List<BaseEntity>();

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    objects.Add(GetEntity(node));
                }
            }
            return objects;
        }

        private BaseEntity GetEntity(HtmlNode node)
        {
            var data = node.ChildNodes.ToArray();
            var firstNodeChilds = data[0].ChildNodes.ToArray();

            var adr = firstNodeChilds[0].Attributes["data-name"]?.Value.Replace(':', ',').Split(", ");

            var bankName = adr[0];
            var branchName = adr[1];
            var branchAdr = string.Join(", ", adr[2..adr.Length]);

            var latitude = firstNodeChilds[0].Attributes["data-x"]?.Value;
            var longitude = firstNodeChilds[0].Attributes["data-y"]?.Value;

            var phones = new List<string>();

            var phonesChilds = firstNodeChilds[2].ChildNodes.ToArray();
            if (!phonesChilds.Length.Equals(0))
            {
                phones = GetPhones(phonesChilds[1].ChildNodes.ToArray());
            }
            
            var bestBuy = data[1]?.InnerText;

            var bestSale = data[2]?.InnerText;

            return new BaseEntity
            {
                BankName = bankName,
                BranchName = branchName,
                BranchAdr = branchAdr,
                Latitude = latitude,
                Longitude = longitude,
                Phone = phones,
                Buy = bestBuy,
                Sale = bestSale
            };
        }

        private List<string> GetPhones(HtmlNode[] nodes)
        {
            List<string> phones = new List<string>();

            foreach (var node in nodes)
            {
                phones.Add(node?.InnerText);
            }

            return phones;
        }
    }
}
