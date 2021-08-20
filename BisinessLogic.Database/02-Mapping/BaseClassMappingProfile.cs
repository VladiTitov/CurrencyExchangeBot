using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database
{
    public class BaseClassMappingProfile : Profile
    {
        public BaseClassMappingProfile() => 
            CreateMap<BaseEntity, BaseClassDTO>().ReverseMap();
    }
}
