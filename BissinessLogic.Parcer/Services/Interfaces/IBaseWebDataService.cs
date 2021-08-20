using System.Collections.Generic;
using BusinessLogic.Database;

namespace BusinessLogic.Parser.Services.Interfaces
{
    interface IBaseWebDataService
    {
        IEnumerable<BaseClassDTO> GetData(string selector, string url);
    }
}
