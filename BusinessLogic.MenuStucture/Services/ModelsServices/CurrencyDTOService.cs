using System.Linq;
using BisinessLogic.Database;

namespace BusinessLogic.MenuStucture.Services.ModelsServices
{
    class CurrencyDTOService
    {
        private readonly ICurrencyService _currencyService;
        private readonly ICityService _cityService;
        private readonly IBranchService _branchService;
        private readonly IQuotationService _quotationService;

        public CurrencyDTOService(
            ICurrencyService currencyService,
            ICityService cityService,
            IBranchService branchService,
            IQuotationService quotationService)
        {
            _currencyService = currencyService;
            _cityService = cityService;
            _branchService = branchService;
            _quotationService = quotationService;
        }

        public string[] GetNames() =>
            _currencyService.GetData().Select(i => i.NameRus).ToArray();

        public string[] GetCurrencies(string city)
        {
            var cityId = _cityService.GetData().FirstOrDefault(i => i.NameRus.Equals(city)).Id;
            var branches = _branchService.GetBranchInCity(cityId).ToList();
            var branchID = branches.Select(i => i.Id).Distinct();
            var quotations = _quotationService.GetData().Where(i => branchID.Contains(i.BranchDtoId)).ToList();
            var currencies = _currencyService.GetData().Where(i => quotations.Select(i => i.CurrencyDtoId).ToList().Contains(i.Id)).ToArray().Select(i => i.NameRus).Distinct().ToArray();
            return currencies;
        }
    }
}
