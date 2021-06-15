using System.Collections.Generic;
using BisinessLogic.Database;

namespace BissinessLogic.Parser
{
    interface ICityWebDataService
    {
        IEnumerable<CityDTO> GetData(string selector, string url);
    }
}
