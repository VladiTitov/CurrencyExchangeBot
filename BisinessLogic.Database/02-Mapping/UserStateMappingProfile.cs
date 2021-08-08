using AutoMapper;
using DataAccess.DataBaseLayer;

namespace BusinessLogic.Database
{
    public class UserStateMappingProfile : Profile
    {
        public UserStateMappingProfile()
        {
            CreateMap<UserState, UserStateDTO>().ReverseMap();
        }
    }
}
