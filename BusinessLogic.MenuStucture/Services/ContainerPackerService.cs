using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Services.ModelsServices;
using SimpleInjector;

namespace BusinessLogic.MenuStucture.Services
{
    class ContainerPackerService
    {
        private Container container;

        public ContainerPackerService() => 
            container = new MenuContainer().CreateContainer();

        public string[] GetCitiesNames() => 
            container.GetInstance<CityDTOService>().GetNames();

        public string[] GetCurrenciesNames() => 
            container.GetInstance<CurrencyDTOService>().GetNames();

        public UserStateDTO GetUserState(long userId) => 
            container.GetInstance<UserStateDTOService>().GetUserState(userId);

        public void SaveUserState(UserStateDTO userState) => 
            container.GetInstance<UserStateDTOService>().SaveState(userState);

        public string[] GetCurrencies(string city) => 
            container.GetInstance<CurrencyDTOService>().GetCurrencies(city);

        public string[] GetBanksNames(string city) => 
            container.GetInstance<BranchDTOService>().GetBanksName(city);
        }
}
