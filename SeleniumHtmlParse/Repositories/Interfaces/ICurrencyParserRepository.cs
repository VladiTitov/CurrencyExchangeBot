using System.Collections.Generic;
using DataAccess.DataBaseLayer;

namespace DataAccess.SeleniumHtmlParse
{
    interface ICurrencyParserRepository
    {
        IEnumerable<Currency> GetCurrencies(string selector, string url);
    }
}
