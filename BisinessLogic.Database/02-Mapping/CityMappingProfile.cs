using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile() => 
            CreateMap<City, CityDTO>().ReverseMap();
    }
}
