using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    public class BranchMappingProfile : Profile
    {
        public BranchMappingProfile()
        {
            CreateMap<Branch, BranchDTO>()
                .ForMember(dst => dst.Adr, opt => opt.MapFrom(src => src.Adr))
                .ForMember(dst => dst.BankDtoId, opt => opt.MapFrom(src => src.BankId))
                .ForMember(dst => dst.CityDtoId, opt => opt.MapFrom(src => src.CityId))
                .ReverseMap();
        }
    }
}
