using System.Collections.Generic;
using BusinessLogic.Database;

namespace BusinessLogic.Parser.Services.Interfaces
{
    interface ICurrencyWebDataService
    {
        IEnumerable<CurrencyDTO> GetData(string selector, string url);
    }
}
