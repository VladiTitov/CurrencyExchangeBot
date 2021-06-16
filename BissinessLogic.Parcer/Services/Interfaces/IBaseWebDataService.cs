using System.Collections.Generic;
using BisinessLogic.Database;

namespace BissinessLogic.Parser
{
    interface IBaseWebDataService
    {
        IEnumerable<BaseClassDTO> GetData(string selector, string url);
    }
}
