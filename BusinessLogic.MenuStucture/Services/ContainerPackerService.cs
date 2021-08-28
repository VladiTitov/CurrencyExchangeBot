using System.Collections.Generic;
using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Services.ModelsDtoServices;
using SimpleInjector;

namespace BusinessLogic.MenuStucture.Services
{
    class ContainerPackerService
    {
        private readonly Container _container;

        public ContainerPackerService() =>
            _container = new MenuContainer().CreateContainer();


        public IEnumerable<CityDTO> GetCities() => 
            _container.GetInstance<BaseModelsService>().GetCities();
        public IEnumerable<BankDTO> GetBanks() =>
            _container.GetInstance<BaseModelsService>().GetBanks();
        public IEnumerable<BranchDTO> GetBranches() => 
            _container.GetInstance<BaseModelsService>().GetBranches();
        public IEnumerable<QuotationDTO> GetQuotations() => 
            _container.GetInstance<BaseModelsService>().GetQuotations();
        public IEnumerable<CurrencyDTO> GetCurrencies() => 
            _container.GetInstance<BaseModelsService>().GetCurrencies();

        public int GetCityId(string name) => 
            _container.GetInstance<BaseModelsService>().GetCitiesId(name);
        public int GetBankId(string name) => 
            _container.GetInstance<BaseModelsService>().GetBanksId(name);
        public int GetBranchId(string name) => 
            _container.GetInstance<BaseModelsService>().GetBranchesId(name);
        public int GetCurrencyId(string name) => 
            _container.GetInstance<BaseModelsService>().GetCurrenciesId(name);

        public UserStateDTO GetUserState(long userId) => 
            _container.GetInstance<UserStateDTOService>().GetUserState(userId);
        public void SaveUserState(UserStateDTO userState) => 
            _container.GetInstance<UserStateDTOService>().SaveState(userState);
    }
}
