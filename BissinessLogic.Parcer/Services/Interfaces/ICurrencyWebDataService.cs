using System.Collections.Generic;
using BisinessLogic.Database;

namespace BissinessLogic.Parser.Services.Interfaces
{
    interface ICurrencyWebDataService
    {
        IEnumerable<CurrencyDTO> GetData(string selector, string url);
    }
}
