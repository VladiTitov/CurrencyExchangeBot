using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database
{
    public class QuotationMappingProfile : Profile
    {
        public QuotationMappingProfile()
        {
            CreateMap<Quotation, QuotationDTO>().ReverseMap();
        }
    }
}
