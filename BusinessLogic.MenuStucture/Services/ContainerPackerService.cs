using System.Collections.Generic;
using BusinessLogic.Database;
using BusinessLogic.MenuStucture.Keyboard.RequestModels;
using SimpleInjector;

namespace BusinessLogic.MenuStucture.Services
{
    class ContainerPackerService
    {
        private readonly Container _container;

        public ContainerPackerService() =>
            _container = new MenuContainer().CreateContainer();

        public string[] GetCitiesList() => 
            _container.GetInstance<GetDataFromBDService>().GetCitiesList();

        public string[] GetBanksNamesByCurrency(int currencyId, int cityId) =>
            _container.GetInstance<GetDataFromBDService>().GetBanksNamesByCurrency(currencyId, cityId);

        public string GetBankNameById(int id) => 
            _container.GetInstance<GetDataFromBDService>().GetBankNameById(id);

        public List<BestOffersModel> GetBranchesAndOffers(int currencyId, int bankId, int cityId, bool key) =>
            _container.GetInstance<GetDataFromBDService>().GetBranchesAndOffers(currencyId, bankId, cityId, key);

        public List<BranchDTO> GetBranchesList(IEnumerable<int> idList) =>
            _container.GetInstance<GetDataFromBDService>().GetBranchesList(idList);

        public IEnumerable<QuotationDTO> GetQuotationsList(int cityId) =>
            _container.GetInstance<GetDataFromBDService>().GetQuotations(cityId);

        public List<BestOffersModel> GetBestOffer(int cityId, int currencyId, bool key) =>
            _container.GetInstance<GetDataFromBDService>().GetBestOffer(cityId, currencyId, key);

        public IEnumerable<BestOffersModel> GetQuotationByBranches(int currencyId, List<BranchDTO> branches, bool key) =>
            _container.GetInstance<GetDataFromBDService>().GetQuotationByBranches(currencyId, branches, key);

        public string[] GetCurrenciesList(int cityId) => 
            _container.GetInstance<GetDataFromBDService>().GetCurrenciesList(cityId);

        public int GetCityId(string name) => 
            _container.GetInstance<GetDataFromBDService>().GetCitiesId(name);
        public int GetBankId(string name) => 
            _container.GetInstance<GetDataFromBDService>().GetBanksId(name);
        public int GetBranchId(string name) => 
            _container.GetInstance<GetDataFromBDService>().GetBranchesId(name);
        public int GetCurrencyId(string name) => 
            _container.GetInstance<GetDataFromBDService>().GetCurrenciesId(name);

        public IEnumerable<CityDTO> GetCities() => 
            _container.GetInstance<GetDataFromBDService>().GetCities();

        public IEnumerable<CurrencyDTO> GetCurrencies() => 
            _container.GetInstance<GetDataFromBDService>().GetCurrencies();

        public IEnumerable<BankDTO> GetBanks() => 
            _container.GetInstance<GetDataFromBDService>().GetBanks();

        public UserStateDTO GetUserState(long userId) => 
            _container.GetInstance<GetDataFromBDService>().GetUserState(userId);
        public void SaveUserState(UserStateDTO userState) => 
            _container.GetInstance<GetDataFromBDService>().SaveState(userState);
    }
}
