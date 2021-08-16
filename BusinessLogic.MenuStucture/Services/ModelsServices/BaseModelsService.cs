using System.Collections.Generic;
using System.Linq;
using BisinessLogic.Database;

namespace BusinessLogic.MenuStucture.Services.ModelsServices
{
    public class BaseModelsService
    {
        private readonly ICityService _cityService;
        private readonly IBankService _bankService;
        private readonly IBranchService _branchService;
        private readonly IQuotationService _quotationService;
        private readonly ICurrencyService _currencyService;

        public BaseModelsService(ICityService cityService,
            IBankService bankService,
            IBranchService branchService,
            IQuotationService quotationService,
            ICurrencyService currencyService)
        {
            _cityService = cityService;
            _bankService = bankService;
            _branchService = branchService;
            _quotationService = quotationService;
            _currencyService = currencyService;
        }

        private int GetCurrencyId(string currencyName) => 
            _currencyService.GetData().FirstOrDefault(i => i.NameRus.Equals($" {currencyName}")).Id;

        private int GetCityId(string cityName) =>
            _cityService.GetData().FirstOrDefault(i => i.NameRus.Equals(cityName)).Id;

        public string[] GetCurrenciesNames() =>
            _currencyService.GetData().Select(i => i.NameRus).ToArray();

        public string[] GetCurrencies(string cityName)
        {
            var cityId = GetCityId(cityName);
            var branches = _branchService.GetBranchInCity(cityId).ToList();
            var branchID = branches.Select(i => i.Id).Distinct();
            var quotations = _quotationService.GetData().Where(i => branchID.Contains(i.BranchDtoId)).ToList();
            var currencies = _currencyService.GetData().Where(i => quotations.Select(i => i.CurrencyDtoId).ToList().Contains(i.Id)).ToArray().Select(i => i.NameRus).Distinct().ToArray();
            return currencies;
        }

        public string[] GetBanksNames() => _bankService.GetData().Select(i => i.NameRus).ToArray();

        public string[] GetCityNames() => _cityService.GetData().Select(i => i.NameRus).ToArray();

        public IEnumerable<BranchDTO> GetBranchesList(string currencyName, string cityName)
        {
            int currencyId = GetCurrencyId(currencyName);
            int cityId = GetCityId(cityName);

            var branchList = _branchService.GetData().Where(i => i.CityDtoId.Equals(cityId)).ToList();
            var quotationList = _quotationService.GetData().Where(i => i.CurrencyDtoId.Equals(currencyId)).Select(i => i.BranchDtoId).ToList();

            List<BranchDTO> result = new List<BranchDTO>();

            foreach (var branch in branchList)
            {
                if (quotationList.Contains(branch.Id)) result.Add(branch);
            }

            foreach (var branch in result)
            {
                branch.Bank = _bankService.GetData().FirstOrDefault(i => i.Id.Equals(branch.BankDtoId));
            }

            return result;
        }

        public string[] GetBranchesAddrList(string currencyName, string cityName) =>
            GetBranchesList(currencyName, cityName).Select(i => i.AdrRus).Distinct().ToArray();

        public string[] GetBanksNamesByCurrency(string currencyName, string cityName) => 
            GetBranchesList(currencyName, cityName).Select(i=>i.Bank.NameRus).Distinct().ToArray();
        

    }
}
