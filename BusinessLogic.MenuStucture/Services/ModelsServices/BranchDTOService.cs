using System.Linq;
using BisinessLogic.Database;

namespace BusinessLogic.MenuStucture.Services.ModelsServices
{
    class BranchDTOService
    {
        private readonly IBranchService _branchService;
        private readonly ICityService _cityService;

        public BranchDTOService(IBranchService branchService,
            ICityService cityService)
        {
            _branchService = branchService;
            _cityService = cityService;
        }

        public string[] GetBanksName(string city)
        {
            var cityId = _cityService.GetData().FirstOrDefault(i => i.NameRus.Equals(city)).Id;
            var banks = _branchService.GetBranchInCity(cityId).Select(i => i.Bank).ToList();
            var names = banks.Select(i => i.NameRus).Distinct().ToArray();
            return names;
        }
    }
}
