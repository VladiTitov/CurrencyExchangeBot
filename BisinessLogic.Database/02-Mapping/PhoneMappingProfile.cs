using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database
{
    public class PhoneMappingProfile : Profile
    {
        public PhoneMappingProfile()
        {
            CreateMap<Phone, PhoneDTO>().ReverseMap();
        }
    }
}
