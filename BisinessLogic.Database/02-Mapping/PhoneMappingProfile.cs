using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    class PhoneMappingProfile : Profile
    {
        public PhoneMappingProfile()
        {
            CreateMap<Phone, PhoneDTO>().ReverseMap();
        }
    }
}
