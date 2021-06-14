using System.Collections.Generic;

namespace BisinessLogic.Database
{
    interface ICityService
    {
        IEnumerable<CityDTO> GetData();
        void Add(CityDTO item);
        void Update(CityDTO item);
        void Delete(CityDTO item);
    }
}
