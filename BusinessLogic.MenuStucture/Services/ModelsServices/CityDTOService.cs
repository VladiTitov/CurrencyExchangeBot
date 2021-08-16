using System.Linq;

namespace BusinessLogic.MenuStucture.Services.ModelsServices
{
    class CityDTOService
    {
        private readonly ContainerPackerService _containerPacker;

        public CityDTOService() => _containerPacker = new ContainerPackerService();

        public string[] GetCitiesList() => 
            _containerPacker.GetCities().Select(i => i.NameRus).ToArray();
    }
}
