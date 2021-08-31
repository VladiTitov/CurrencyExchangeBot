using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database.Interfaces
{
    public interface ICityService
    {
        IEnumerable<CityDTO> GetData();
        Task<IEnumerable<CityDTO>> GetDataAsync();
        IEnumerable<City> GetDataTemp();
        void Add(CityDTO item);
        void Update(CityDTO item);
        void Delete(CityDTO item);
    }
}
