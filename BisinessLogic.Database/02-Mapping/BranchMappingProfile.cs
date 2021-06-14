using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    public class BranchMappingProfile : Profile
    {
        public BranchMappingProfile()
        {
            CreateMap<Branch, BranchDTO>().ReverseMap();
        }
    }
}
