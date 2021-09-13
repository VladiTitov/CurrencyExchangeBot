using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDTO>> GetData();
        Task Add(CityDTO item);
    }
}
