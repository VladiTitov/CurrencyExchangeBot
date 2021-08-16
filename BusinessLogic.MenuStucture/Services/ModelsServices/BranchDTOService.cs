using System.Collections.Generic;
using System.Linq;
using BisinessLogic.Database;

namespace BusinessLogic.MenuStucture.Services.ModelsServices
{
    class BranchDTOService
    {
        private readonly ContainerPackerService _packerService;

        public BranchDTOService() => _packerService = new ContainerPackerService();

        public List<BranchDTO> GetBranchesList(string currencyName, string cityName)
        {
            int currencyId = _packerService.GetCurrencyId(currencyName);
            int cityId = _packerService.GetCityId(cityName);

            var branchList = _packerService.GetBranches().Where(i => i.CityDtoId.Equals(cityId)).ToList();
            var quotationList = _packerService.GetQuotations().Where(i => i.CurrencyDtoId.Equals(currencyId)).Select(i => i.BranchDtoId).ToList();

            List<BranchDTO> result = new List<BranchDTO>();

            foreach (var branch in branchList)
            {
                if (quotationList.Contains(branch.Id)) result.Add(branch);
            }

            foreach (var branch in result)
            {
                branch.Bank = _packerService.GetBanks().FirstOrDefault(i => i.Id.Equals(branch.BankDtoId));
            }

            return result;
        }

        public string[] GetBranchesAddrList(string currencyName, string cityName) =>
            GetBranchesList(currencyName, cityName).Select(i => i.AdrRus).Distinct().ToArray();

        public IEnumerable<BranchDTO> GetBranchesInCity(int id)
        {
            var cities = _packerService.GetCities();
            var banks = _packerService.GetBanks();

            var items = _packerService.GetBranches().Where(i => i.CityDtoId.Equals(id));
            foreach (var item in items)
            {
                item.City = cities.FirstOrDefault(i => i.Id.Equals(item.CityDtoId));
                item.Bank = banks.FirstOrDefault(i=> i.Id.Equals(item.BankDtoId));
            }
            return items;
        }
    }

}
