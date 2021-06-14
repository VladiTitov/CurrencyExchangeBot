using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BisinessLogic.Database
{
    class BranchMappingProfile : Profile
    {
        public BranchMappingProfile()
        {
            CreateMap<Branch, BranchDTO>().ReverseMap();
        }
    }
}
