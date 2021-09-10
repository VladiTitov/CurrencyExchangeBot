using System;
using BusinessLogic.Database;
using BusinessLogic.Database.Interfaces;
using BusinessLogic.Parser.Services.Interfaces;

namespace BusinessLogic.Parser
{
    class Parser
    {
        private static IBankService _bankService;
        private static IBranchService _branchService;
        private readonly ICityService _cityService;
        private readonly ICurrencyService _currencyService;
        private static IPhoneService _phoneService;
        private static IQuotationService _quotationService;

        private static IBaseWebDataService _baseWebDataService;

        public Parser(IBankService bankService,
            IBranchService branchService,
            ICityService cityService,
            ICurrencyService currencyService,
            IPhoneService phoneService,
            IQuotationService quotationService,
            IBaseWebDataService baseWebDataService)
        {
            _bankService = bankService;
            _branchService = branchService;
            _cityService = cityService;
            _currencyService = currencyService;
            _phoneService = phoneService;
            _quotationService = quotationService;
            _baseWebDataService = baseWebDataService;
        }


        public async void Start()
        {
            var cities = await _cityService.GetData();
            var currencies = await _currencyService.GetData();

            foreach (var city in cities)
            {
                foreach (var currency in currencies)
                {
                    Console.Clear();
                    Console.WriteLine(
                        $"{new string('#', 40)}\n{city.NameRus} {currency.NameRus}\n{new string('#', 40)}");
                    var baseClassList = _baseWebDataService.GetData(
                        selector: ".//*/tbody/tr/td/table/tbody/tr/td",
                        url: @"https://select.by" + $"/{city.NameLat}{currency.Url}");
                    foreach (var baseClass in baseClassList)
                    {
                        BankDTO bank = new BankDTO
                        {
                            NameRus = baseClass.BankName
                        };

                        await _bankService.Add(bank);

                        var bankDto = await _bankService.GetWithInclude(bank);

                        BranchDTO branch = new BranchDTO
                        {
                            Name = baseClass.BranchName,
                            Adr = baseClass.BranchAdr,
                            Latitude = baseClass.Latitude,
                            Longitude = baseClass.Longitude,
                            BankId = bankDto.Id,
                            CityId = city.Id
                        };
                        await _branchService.Add(branch);

                        var branchDto = await _branchService.GetWithInclude(branch);

                        QuotationDTO quotation = new QuotationDTO
                        {
                            Sale = baseClass.Sale,
                            Buy = baseClass.Buy,
                            BranchId = branchDto.Id,
                            CurrencyId = currency.Id
                        };
                        await _quotationService.Add(quotation);

                        foreach (var phoneString in baseClass.Phone)
                        {
                            if (!phoneString.Equals(""))
                            {
                                PhoneDTO phone = new PhoneDTO
                                {
                                    PhoneNum = phoneString,
                                    BranchId = branchDto.Id
                                };
                                await _phoneService.Add(phone);
                            }
                        }
                    }
                }
            }
        }
    }
}
