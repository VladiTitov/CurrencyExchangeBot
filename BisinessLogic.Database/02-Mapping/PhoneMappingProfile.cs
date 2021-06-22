using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    public class PhoneMappingProfile : Profile
    {
        public PhoneMappingProfile()
        {
            CreateMap<Phone, PhoneDTO>()
                .ForMember(dst=>dst.PhoneNum, opt=>opt.MapFrom(src=>src.PhoneNum))
                .ForMember(dst => dst.BranchDtoId, opt => opt.MapFrom(src => src.BranchId))
                .ReverseMap();
        }
    }
}
