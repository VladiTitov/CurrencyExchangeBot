using DataAccess.DataBaseLayer;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace DataAccess.SeleniumHtmlParse.Repositories.Classes
{
    public class CityParserRepository : ICityParserRepository
    {
        public IEnumerable<City> GetCities(string selector, string url)
        {
            using (var parserData = new GenericRepository(url))
            {
                var dataWebElements = parserData.GetDataList(By.XPath(selector));
                var resultCities = new List<City>();

                for (int i = 1; i < dataWebElements.Count; i++)
                {
                    var tempUrl = dataWebElements[i].GetAttribute("value");
                    var nameLat = GetNameLatCity(tempUrl);
                    var nameRus = dataWebElements[i].Text;

                    resultCities.Add(new City
                    {
                        NameLat = nameLat,
                        NameRus = nameRus,
                        Url = tempUrl
                    });
                }
                return resultCities;
            }
        }

        private string GetNameLatCity(string url)
        {
            string tempName = url.Split('/')[1];
            char firstChar = tempName[0];
            return $"{firstChar.ToString().ToUpper()}{tempName.TrimStart(firstChar)}";
        }
    }
}
