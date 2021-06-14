using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<City, CityDTO>().ReverseMap();
        }
    }
}
