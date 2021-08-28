using BusinessLogic.MenuStucture.Constants;
using System.Linq;

namespace BusinessLogic.MenuStucture.Services.ModelsDtoServices
{
    class CityDTOService
    {
        private readonly ContainerPackerService _containerPacker;

        public CityDTOService() => _containerPacker = new ContainerPackerService();

        public string[] GetCitiesList() 
        {
            var cities = _containerPacker.GetCities().Select(i => i.NameRus).ToArray();
            for (int i = 0; i < cities.Length; i++)
            {
                cities[i] = $"{MenuEmojiConstants.City}  {cities[i]}";
            }

            return cities;
        }
            
    }
}
