using System.Collections.Generic;
using BusinessLogic.Database;

namespace BusinessLogic.Parser.Services.Interfaces
{
    interface ICityWebDataService
    {
        IEnumerable<CityDTO> GetData(string selector, string url);
    }
}
