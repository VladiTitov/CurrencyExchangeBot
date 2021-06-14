using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    public class BankMappingProfile : Profile
    {
        public BankMappingProfile()
        {
            CreateMap<Bank, BankDTO>().ReverseMap();
        }
    }
}
