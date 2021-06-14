using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    class BankMappingProfile : Profile
    {
        public BankMappingProfile()
        {
            CreateMap<Bank, BankDTO>().ReverseMap();
        }
    }
}
