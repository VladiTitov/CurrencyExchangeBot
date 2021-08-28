using BusinessLogic.Database;
using BusinessLogic.Database.Interfaces;
using System.Linq;

namespace BusinessLogic.MenuStucture.Services.ModelsDtoServices
{
    class UserStateDTOService
    {
        private readonly IUserStateService _userStateService;

        public UserStateDTOService(IUserStateService userStateService) => _userStateService = userStateService;

        public UserStateDTO GetUserState(long userId) => _userStateService.GetState(userId) ?? new UserStateDTO() { UserId = userId };

        public void SaveState(UserStateDTO userState) 
        {
            var isExist = _userStateService.GetData().FirstOrDefault(i=>i.UserId.Equals(userState.UserId));
            if (isExist != null) _userStateService.Update(userState);
            else _userStateService.Add(userState);
        } 
    }
}
