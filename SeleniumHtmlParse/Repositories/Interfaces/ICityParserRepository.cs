using System.Collections.Generic;
using DataAccess.DataBaseLayer;

namespace DataAccess.SeleniumHtmlParse
{
    public interface ICityParserRepository
    {
        IEnumerable<City> GetCities(string selector, string url);
    }
}
