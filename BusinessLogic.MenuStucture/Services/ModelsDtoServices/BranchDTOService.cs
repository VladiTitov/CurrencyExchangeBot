using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Database;

namespace BusinessLogic.MenuStucture.Services.ModelsDtoServices
{
    class BranchDTOService
    {
        private readonly ContainerPackerService _packerService;

        public BranchDTOService() => _packerService = new ContainerPackerService();

        public List<BranchDTO> GetBranchesList(int currencyId, int cityId)
        {
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

        public string[] GetBranchesAndOffers(int currencyId, int cityId, int bankId, bool key)
        {
            var branches = GetBranchesList(currencyId, cityId).Where(i => i.BankDtoId.Equals(bankId)).ToList();
            return new QuotationDTOService().GetQuotationByBranches(currencyId, branches, key);
        }

        public List<BranchDTO> GetBranchesList(List<int> idList)
        {
            var branchList = _packerService.GetBranches().Where(i => idList.Contains(i.Id)).Distinct().ToList();
            foreach (var branch in branchList)
            {
                branch.Bank = _packerService.GetBanks().FirstOrDefault(i => i.Id.Equals(branch.BankDtoId));
            }

            return branchList;
        }

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
