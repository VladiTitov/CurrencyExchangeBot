using System.Collections.Generic;
using DataAccess.DataBaseLayer;

namespace DataAccess.SeleniumHtmlParse
{
    interface ICityParserRepository
    {
        IEnumerable<City> GetCities(string selector, string url);
    }
}
