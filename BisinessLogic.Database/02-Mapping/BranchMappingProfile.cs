using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    public class BranchMappingProfile : Profile
    {
        public BranchMappingProfile()
        {
            CreateMap<Branch, BranchDTO>()
                .ForMember(dst => dst.AdrRus, opt => opt.MapFrom(src => src.AdrRus))
                .ForMember(dst => dst.BankDtoId, opt => opt.MapFrom(src => src.BankId))
                .ForMember(dst=>dst.CityDtoId, opt=>opt.MapFrom(src=>src.CityId))
                .ReverseMap();
        }
    }
}
