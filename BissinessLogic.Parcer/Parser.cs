using System;
using System.Threading.Tasks;
using BusinessLogic.Database;
using BusinessLogic.Database.Interfaces;
using BusinessLogic.Parser.Services.Interfaces;

namespace BusinessLogic.Parser
{
    class Parser
    {
        private readonly IBankService _bankService;
        private readonly IBranchService _branchService;
        private readonly ICityService _cityService;
        private readonly ICurrencyService _currencyService;
        private readonly IPhoneService _phoneService;
        private readonly IQuotationService _quotationService;

        private readonly IBaseWebDataService _baseWebDataService;

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


        public async Task StartAsync()
        {
            var cities = _cityService.GetData();
            var currencies = _currencyService.GetData();

            foreach (var city in cities)
            {
                foreach (var currency in currencies)
                {
                    Console.Clear();
                    Console.WriteLine($"{new string('#', 40)}\n{city.NameRus} {currency.NameRus}\n{new string('#', 40)}");
                    var baseClassList = _baseWebDataService.GetData(
                        selector: ".//*/tbody/tr/td/table/tbody/tr/td",
                        url: @"https://select.by" + $"/{city.NameLat}{currency.Url}");
                    foreach (var baseClass in baseClassList)
                    {
                        BankDTO bank = new BankDTO { NameRus = baseClass.BankName };
                        _bankService.Add(bank);

                        BranchDTO branch = new BranchDTO
                        {
                            Name = baseClass.BranchName,
                            Adr = baseClass.BranchAdr,
                            Bank = bank,
                            City = city
                        };
                        _branchService.Add(branch);

                        QuotationDTO quotation = new QuotationDTO
                        {
                            Sale = baseClass.Sale,
                            Buy = baseClass.Buy,
                            Branch = branch,
                            Currency = currency
                        };
                        _quotationService.Add(quotation);

                        foreach (var phoneString in baseClass.Phone)
                        {
                            if (!phoneString.Equals(""))
                            {
                                PhoneDTO phone = new PhoneDTO
                                {
                                    PhoneNum = phoneString,
                                    Branch = branch
                                };
                                _phoneService.Add(phone);
                            }
                        }
                    }
                }
            }
        }
    }
}
