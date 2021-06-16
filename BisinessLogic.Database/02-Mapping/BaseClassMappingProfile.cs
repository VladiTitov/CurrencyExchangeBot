using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    public class BaseClassMappingProfile : Profile
    {
        public BaseClassMappingProfile()
        {
            CreateMap<BaseEntity, BaseClassDTO>().ReverseMap();
        }
    }
}
