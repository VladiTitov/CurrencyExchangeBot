using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.Database;
using BusinessLogic.Parser.Services.Interfaces;
using DataAccess.SeleniumHtmlParse;

namespace BusinessLogic.Parser.Services.Classes
{
    class CityWebDataService : ICityWebDataService
    {
        private readonly ICityParserRepository _cityParserRepository;
        private readonly IMapper _mapper;

        public CityWebDataService(ICityParserRepository cityParserRepository, IMapper mapper)
        {
            _cityParserRepository = cityParserRepository;
            _mapper = mapper;
        }

        public IEnumerable<CityDTO> GetData(string selector, string url) =>
            _mapper.Map<List<CityDTO>>(_cityParserRepository.GetCities(selector, url));
    }
}
