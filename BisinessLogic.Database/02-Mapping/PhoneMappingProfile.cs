using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    public class PhoneMappingProfile : Profile
    {
        public PhoneMappingProfile()
        {
            CreateMap<Phone, PhoneDTO>().ReverseMap();
        }
    }
}
