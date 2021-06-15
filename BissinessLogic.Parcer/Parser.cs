using BisinessLogic.Database;
using BissinessLogic.Parser.Services.Interfaces;

namespace BissinessLogic.Parser
{
    class Parser
    {
        private readonly IBankService _bankService;
        private readonly IBranchService _branchService;
        private readonly ICityService _cityService;
        private readonly ICurrencyService _currencyService;
        private readonly IPhoneService _phoneService;
        private readonly IQuotationService _quotationService;

        private readonly ICurrencyWebDataService _currencyWebDataService;
        private readonly ICityWebDataService _cityWebDataService;

        public Parser(IBankService bankService,
            IBranchService branchService,
            ICityService cityService,
            ICurrencyService currencyService,
            IPhoneService phoneService,
            IQuotationService quotationService,
            ICityWebDataService cityWebDataService,
            ICurrencyWebDataService currencyWebDataService)
        {
            _bankService = bankService;
            _branchService = branchService;
            _cityService = cityService;
            _currencyService = currencyService;
            _phoneService = phoneService;
            _quotationService = quotationService;

            _cityWebDataService = cityWebDataService;
            _currencyWebDataService = currencyWebDataService;
        }


        public void Start()
        {
            var cities = _cityWebDataService.GetData(selector: ".//*/li/select/option", url: @"https://m.select.by/kurs");
            foreach (var city in cities) _cityService.Add(city);

            var currencies = _currencyWebDataService.GetData(selector: ".//*/div/select/option", url: @"https://m.select.by/kurs");
            foreach (var currency in currencies) _currencyService.Add(currency);
        }
    }
}
