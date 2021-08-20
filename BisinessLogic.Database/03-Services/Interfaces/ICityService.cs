using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Database.Interfaces
{
    public interface ICityService
    {
        IEnumerable<CityDTO> GetData();
        Task Add(CityDTO item);
        Task Update(CityDTO item);
        Task Delete(CityDTO item);
    }
}
