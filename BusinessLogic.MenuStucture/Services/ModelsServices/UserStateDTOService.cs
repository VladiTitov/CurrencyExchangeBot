using BusinessLogic.Database;

namespace BusinessLogic.MenuStucture.Services.ModelsServices
{
    class UserStateDTOService
    {
        private readonly IUserStateService _userStateService;

        public UserStateDTOService(IUserStateService userStateService) => _userStateService = userStateService;

        public UserStateDTO GetUserState(long userId) => _userStateService.GetState(userId) ?? new UserStateDTO() { UserId = userId };

        public void SaveState(UserStateDTO userState) => _userStateService.Add(userState);
    }
}
