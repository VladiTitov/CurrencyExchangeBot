using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    public class QuotationMappingProfile : Profile
    {
        public QuotationMappingProfile()
        {
            CreateMap<Quotation, QuotationDTO>()
                .ForMember(dst=>dst.Buy, opt=>opt.MapFrom(src=>src.Buy))
                .ForMember(dst=>dst.Sale, opt=>opt.MapFrom(src=>src.Sale))
                .ForMember(dst=>dst.BranchDtoId, opt=>opt.MapFrom(src=>src.BranchId))
                .ForMember(dst=>dst.CurrencyDtoId, opt=>opt.MapFrom(src=>src.CurrencyId))
                .ReverseMap();
        }
    }
}
