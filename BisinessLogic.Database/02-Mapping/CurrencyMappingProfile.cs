using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    class CurrencyMappingProfile : Profile
    {
        public CurrencyMappingProfile()
        {
            CreateMap<Currency, CurrencyDTO>().ReverseMap();
        }
    }
}
