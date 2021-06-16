using System.Collections.Generic;
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
        private readonly IBaseWebDataService _baseWebDataService;

        public Parser(IBankService bankService,
            IBranchService branchService,
            ICityService cityService,
            ICurrencyService currencyService,
            IPhoneService phoneService,
            IQuotationService quotationService,
            ICityWebDataService cityWebDataService,
            ICurrencyWebDataService currencyWebDataService,
            IBaseWebDataService baseWebDataService)
        {
            _bankService = bankService;
            _branchService = branchService;
            _cityService = cityService;
            _currencyService = currencyService;
            _phoneService = phoneService;
            _quotationService = quotationService;

            _cityWebDataService = cityWebDataService;
            _currencyWebDataService = currencyWebDataService;
            _baseWebDataService = baseWebDataService;
        }


        public void Start()
        {
            var cities = _cityWebDataService.GetData(selector: ".//*/li/select/option", url: @"https://m.select.by/kurs");
            foreach (var city in cities) _cityService.Add(city);

            var currencies = _currencyWebDataService.GetData(selector: ".//*/div/select/option", url: @"https://m.select.by/kurs");
            foreach (var currency in currencies) _currencyService.Add(currency); 
            
            GetData(_cityService.GetData(), _currencyService.GetData());
        }

        private void GetData(IEnumerable<CityDTO> cities, IEnumerable<CurrencyDTO> currencies)
        {
            foreach (var city in cities)
            {
                foreach (var currency in currencies)
                {
                    var data = _baseWebDataService.GetData(
                        selector: ".//*/tbody/tr/td/table/tbody/tr/td",
                        url: @"https://select.by" + $"/{city.NameLat}{currency.Url}");
                    foreach (var d in data)
                    {
                        var (bank, branch, quotation, phone) = GetObjects(d);

                        _bankService.Add(bank);
                        _branchService.Add(new BranchDTO
                        {

                        });
                       
                    }
                }
            }
        }

        private (BankDTO bank, BranchDTO branch, QuotationDTO quotation, PhoneDTO phone) GetObjects(BaseClassDTO baseEntity)
        {
            return (
                new BankDTO
                {
                    NameRus = baseEntity.BankName
                },
                new BranchDTO
                {
                    AdrRus = baseEntity.Adr
                },
                new QuotationDTO
                {
                    Buy = baseEntity.Buy,
                    Sale = baseEntity.Sale
                },
                new PhoneDTO
                {
                    PhoneNum = baseEntity.Phone
                });
        }
    }
}
