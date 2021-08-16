using System.Collections.Generic;
using BisinessLogic.Database;
using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Services.ModelsServices;
using SimpleInjector;

namespace BusinessLogic.MenuStucture.Services
{
    class ContainerPackerService
    {
        private readonly Container container;

        public ContainerPackerService() =>
            container = new MenuContainer().CreateContainer();


        public IEnumerable<CityDTO> GetCities() => container.GetInstance<BaseModelsService>().GetCities();
        public IEnumerable<BankDTO> GetBanks() => container.GetInstance<BaseModelsService>().GetBanks();
        public IEnumerable<BranchDTO> GetBranches() => container.GetInstance<BaseModelsService>().GetBranches();
        public IEnumerable<QuotationDTO> GetQuotations() => container.GetInstance<BaseModelsService>().GetQuotations();
        public IEnumerable<CurrencyDTO> GetCurrencies() => container.GetInstance<BaseModelsService>().GetCurrencies();

        public int GetCityId(string name) => container.GetInstance<BaseModelsService>().GetCitiesId(name);
        public int GetBankId(string name) => container.GetInstance<BaseModelsService>().GetBanksId(name);
        public int GetBranchId(string name) => container.GetInstance<BaseModelsService>().GetBranchesId(name);
        public int GetCurrencyId(string name) => container.GetInstance<BaseModelsService>().GetCurrenciesId(name);

        public UserStateDTO GetUserState(long userId) => container.GetInstance<UserStateDTOService>().GetUserState(userId);
        public void SaveUserState(UserStateDTO userState) => container.GetInstance<UserStateDTOService>().SaveState(userState);
    }
}
