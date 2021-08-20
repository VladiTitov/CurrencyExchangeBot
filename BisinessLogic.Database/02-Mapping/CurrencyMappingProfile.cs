using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database
{
    public class CurrencyMappingProfile : Profile
    {
        public CurrencyMappingProfile() => 
            CreateMap<Currency, CurrencyDTO>().ReverseMap();
    }
}
