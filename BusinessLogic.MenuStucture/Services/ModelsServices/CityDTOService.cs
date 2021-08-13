using System.Linq;
using BisinessLogic.Database;

namespace BusinessLogic.MenuStucture.Services.ModelsServices
{
    class CityDTOService
    {
        private readonly ICityService _cityService;

        public CityDTOService(ICityService cityService) => _cityService = cityService;

        public string[] GetNames() =>
            _cityService.GetData().Select(i => i.NameRus).ToArray();
    }
}
