using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Database.Interfaces
{
    public interface ICityService
    {
        IEnumerable<CityDTO> GetData();
        void Add(CityDTO item);
        void Update(CityDTO item);
        void Delete(CityDTO item);
    }
}
