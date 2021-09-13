using System.Collections.Generic;
using DataAccess.DataBaseLayer;

namespace DataAccess.SeleniumHtmlParse
{
    public interface IBaseParserRepository
    {
        IEnumerable<BaseEntity> GetData(string selector, string url);
    }
}
