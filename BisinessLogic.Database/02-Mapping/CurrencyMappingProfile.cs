using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    public class CurrencyMappingProfile : Profile
    {
        public CurrencyMappingProfile()
        {
            CreateMap<Currency, CurrencyDTO>().ReverseMap();
        }
    }
}
