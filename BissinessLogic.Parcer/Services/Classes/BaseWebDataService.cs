using BisinessLogic.Database;
using System.Collections.Generic;
using AutoMapper;
using DataAccess.SeleniumHtmlParse;

namespace BissinessLogic.Parser
{
    class BaseWebDataService : IBaseWebDataService
    {
        private readonly IBaseParserRepository _baseParserRepository;
        private readonly IMapper _mapper;

        public BaseWebDataService(IBaseParserRepository baseParserRepository, IMapper mapper)
        {
            _baseParserRepository = baseParserRepository;
            _mapper = mapper;
        }

        public IEnumerable<BaseClassDTO> GetData(string selector, string url) =>
            _mapper.Map<List<BaseClassDTO>>(_baseParserRepository.GetData(selector, url));
    }
}
