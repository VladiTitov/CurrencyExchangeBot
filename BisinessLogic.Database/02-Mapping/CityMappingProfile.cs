using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<City, CityDTO>().ReverseMap();
        }
    }
}
