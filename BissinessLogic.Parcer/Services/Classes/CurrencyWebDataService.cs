using System.Collections.Generic;
using AutoMapper;
using BisinessLogic.Database;
using BissinessLogic.Parser.Services.Interfaces;
using DataAccess.SeleniumHtmlParse;

namespace BissinessLogic.Parser.Services.Classes
{
    class CurrencyWebDataService : ICurrencyWebDataService
    {
        private readonly ICurrencyParserRepository _currencyParserRepository;
        private readonly IMapper _mapper;

        public CurrencyWebDataService(ICurrencyParserRepository currencyParserRepository, IMapper mapper)
        {
            _currencyParserRepository = currencyParserRepository;
            _mapper = mapper;
        }

        public IEnumerable<CurrencyDTO> GetData(string selector, string url) =>
            _mapper.Map<List<CurrencyDTO>>(_currencyParserRepository.GetCurrencies(selector, url));
    }
}
