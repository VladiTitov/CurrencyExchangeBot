using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Database;
using BusinessLogic.Database.Interfaces;

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

        public IEnumerable<CityDTO> GetCities() => _cityService.GetData();
        public IEnumerable<BankDTO> GetBanks() => _bankService.GetData();
        public IEnumerable<BranchDTO> GetBranches() => _branchService.GetData();
        public IEnumerable<QuotationDTO> GetQuotations() => _quotationService.GetData();
        public IEnumerable<CurrencyDTO> GetCurrencies() => _currencyService.GetData();

        public int GetCitiesId(string name) => GetCities().FirstOrDefault(i=>i.NameRus.Equals(name)).Id;
        public int GetBanksId(string name) => GetBanks().FirstOrDefault(i => i.NameRus.Equals(name)).Id;
        public int GetBranchesId(string name) => GetBranches().FirstOrDefault(i => i.Adr.Equals(name)).Id;
        public int GetCurrenciesId(string name) => GetCurrencies().FirstOrDefault(i => i.NameRus.Equals($" {name}")).Id;
    }
}
