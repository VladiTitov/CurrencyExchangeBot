using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Database;
using BusinessLogic.GeoParser.Models;
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

        public List<BestOffersModel> GetBestOffer(int cityId, int currencyId, bool key) =>
            _container.GetInstance<GetDataFromBDService>().GetBestOffer(cityId, currencyId, key);

        public IEnumerable<BestOffersModel> GetQuotationByBranches(int currencyId, List<BranchDTO> branches, bool key) =>
            _container.GetInstance<GetDataFromBDService>().GetQuotationByBranches(currencyId, branches, key);

        public GeoLocationModel GetLocation(int branchId) =>
            _container.GetInstance<GetDataFromBDService>().GetCoordinates(branchId);

        public string[] GetCurrenciesList(int cityId) => 
            _container.GetInstance<GetDataFromBDService>().GetCurrenciesList(cityId);

        public BankViewModel GetBankFromBranch(int branchId, int currencyId) =>
            _container.GetInstance<GetDataFromBDService>().GetBankFromBranch(branchId, currencyId);

        public int GetCityId(string name) => 
            _container.GetInstance<GetDataFromBDService>().GetCitiesId(name);
        public int GetBankId(string name) => 
            _container.GetInstance<GetDataFromBDService>().GetBanksId(name);
        public int GetBranchId(string name) => 
            _container.GetInstance<GetDataFromBDService>().GetBranchesId(name);
        public int GetCurrencyId(string name) => 
            _container.GetInstance<GetDataFromBDService>().GetCurrenciesId(name);

        public async Task<IEnumerable<CityDTO>> GetCities() => 
            await _container.GetInstance<GetDataFromBDService>().GetCitiesAsync();

        public async Task<IEnumerable<CurrencyDTO>> GetCurrencies() => 
            await _container.GetInstance<GetDataFromBDService>().GetCurrenciesAsync();

        public Task<IEnumerable<BankDTO>> GetBanksAsync() => 
            _container.GetInstance<GetDataFromBDService>().GetBanksAsync();

        public UserStateDTO GetUserState(long userId) => 
            _container.GetInstance<GetDataFromBDService>().GetUserState(userId);

        public void SaveUserState(UserStateDTO userState) => 
            _container.GetInstance<GetDataFromBDService>().SaveState(userState);
    }
}
