using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    class QuotationMappingProfile : Profile
    {
        public QuotationMappingProfile()
        {
            CreateMap<Quotation, QuotationDTO>().ReverseMap();
        }
    }
}
