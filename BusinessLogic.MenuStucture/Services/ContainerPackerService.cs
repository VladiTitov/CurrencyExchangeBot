using System.Collections.Generic;
using BisinessLogic.Database;
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
            container.GetInstance<BaseModelsService>().GetCityNames();

        public string[] GetCurrenciesNames() => 
            container.GetInstance<BaseModelsService>().GetCurrenciesNames();

        public UserStateDTO GetUserState(long userId) => 
            container.GetInstance<UserStateDTOService>().GetUserState(userId);

        public void SaveUserState(UserStateDTO userState) => 
            container.GetInstance<UserStateDTOService>().SaveState(userState);

        public string[] GetCurrencies(string city) => 
            container.GetInstance<BaseModelsService>().GetCurrencies(city);

        public string[] GetBanksNames() =>
            container.GetInstance<BaseModelsService>().GetBanksNames();

        public string[] GetBranchesList(string currencyName, string cityName) =>
            container.GetInstance<BaseModelsService>().GetBranchesAddrList(currencyName, cityName);

        public string[] GetBanksByCurrency(string currencyName, string cityName) =>
        container.GetInstance<BaseModelsService>().GetBanksNamesByCurrency(currencyName, cityName);
        }
}
