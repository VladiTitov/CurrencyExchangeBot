using System.Collections.Generic;
using DataAccess.DataBaseLayer;

namespace DataAccess.SeleniumHtmlParse
{
    public interface ICurrencyParserRepository
    {
        IEnumerable<Currency> GetCurrencies(string selector, string url);
    }
}
