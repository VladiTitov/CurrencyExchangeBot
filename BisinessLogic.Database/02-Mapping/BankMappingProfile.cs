using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database
{
    public class BankMappingProfile : Profile
    {
        public BankMappingProfile() => 
            CreateMap<Bank, BankDTO>().ReverseMap();
    }
}
