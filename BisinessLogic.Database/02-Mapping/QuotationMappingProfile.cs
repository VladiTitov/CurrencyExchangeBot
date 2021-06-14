using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    public class QuotationMappingProfile : Profile
    {
        public QuotationMappingProfile()
        {
            CreateMap<Quotation, QuotationDTO>().ReverseMap();
        }
    }
}
