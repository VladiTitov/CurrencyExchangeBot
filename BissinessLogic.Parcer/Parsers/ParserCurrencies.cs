using BusinessLogic.Database.Interfaces;
using BusinessLogic.Parser.Services.Interfaces;

namespace BusinessLogic.Parser.Parsers
{
    class ParserCurrencies
    {
        private readonly ICurrencyService _currencyService;
        private readonly ICurrencyWebDataService _currencyWebDataService;

        public ParserCurrencies(ICurrencyService currencyService, ICurrencyWebDataService currencyWebDataService)
        {
            _currencyService = currencyService;
            _currencyWebDataService = currencyWebDataService;
        }

        public void Start()
        {
            var currencies = _currencyWebDataService.GetData(selector: ".//*/div/select/option", url: @"https://m.select.by/kurs");
            foreach (var currency in currencies)
            {
                _currencyService.Add(currency);
            }
        }
    }
}
