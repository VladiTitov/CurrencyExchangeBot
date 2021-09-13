using System.Linq;
using System.Collections.Generic;
using BusinessLogic.Database;
using BusinessLogic.Database.Interfaces;
using BusinessLogic.MenuStucture.Constants;
using BusinessLogic.MenuStucture.Keyboard.RequestModels;
using System.Threading.Tasks;
using BusinessLogic.GeoParser.Models;

namespace BusinessLogic.MenuStucture.Services
{
    class GetDataFromBDService
    {
        private class BestOffers
        {
            public int BranchId { get; }
            public string Offer { get; }
            public BestOffers(int branchId, string offer) => 
                (BranchId, Offer) = (branchId, offer);
        }

        private readonly ICityService _cityService;
        private readonly IBankService _bankService;
        private readonly IBranchService _branchService;
        private readonly IQuotationService _quotationService;
        private readonly ICurrencyService _currencyService;
        private readonly IPhoneService _phoneService;
        private readonly IUserStateService _userStateService;

        public GetDataFromBDService(ICityService cityService,
            IBankService bankService,
            IBranchService branchService,
            IQuotationService quotationService,
            ICurrencyService currencyService,
            IPhoneService phoneService,
            IUserStateService userStateService)
        {
            _cityService = cityService;
            _bankService = bankService;
            _branchService = branchService;
            _quotationService = quotationService;
            _currencyService = currencyService;
            _phoneService = phoneService;
            _userStateService = userStateService;
        }

        #region Base CRUD
        public async Task<IEnumerable<CityDTO>> GetCitiesAsync() => await _cityService.GetData();
        public async Task<IEnumerable<BankDTO>> GetBanksAsync() => await _bankService.GetData();
        public async Task<IEnumerable<CurrencyDTO>> GetCurrenciesAsync() => await _currencyService.GetData();
        public async Task<IEnumerable<BranchDTO>> GetBranchesAsync() => await _branchService.GetData();
        public async Task<IEnumerable<QuotationDTO>> GetQuotationsAsync() => await _quotationService.GetData();
        public async Task<IEnumerable<PhoneDTO>> GetPhonesAsync() => await _phoneService.GetData();

        public int GetCitiesId(string name) =>
            GetCitiesAsync().Result.FirstOrDefault(i => i.NameRus.Equals(name)).Id;
        public int GetBanksId(string name) =>
            GetBanksAsync().Result.FirstOrDefault(i => i.NameRus.Equals(name)).Id;
        public int GetBranchesId(string name) => 
            GetBranchesAsync().Result.FirstOrDefault(i => i.Adr.Equals(name)).Id;
        public int GetCurrenciesId(string name) =>
            GetCurrenciesAsync().Result.FirstOrDefault(i => i.NameRus.Equals(name)).Id;


        #endregion

        #region CitiesRegion
        public string[] GetCitiesList()
        {
            var cities = GetCitiesAsync().Result.Select(i => i.NameRus).ToArray();
            for (int i = 0; i < cities.Length; i++)
            {
                cities[i] = $"{MenuEmojiConstants.City}  {cities[i]}";
            }
            return cities;
        }
        #endregion

        #region BanksRegion

        public string[] GetBanksNamesByCurrency(int currencyId, int cityId)
        {
            var banks = GetBranchesList(currencyId, cityId).Select(i => i.Bank.NameRus).Distinct().ToArray();
            for (int i = 0; i < banks.Length; i++)
            {
                banks[i] = $"{MenuEmojiConstants.Bank}  {banks[i]}";
            }
            return banks;
        }

        public string GetBankNameById(int id) => GetBanksAsync().Result.FirstOrDefault(i => i.Id.Equals(id))?.NameRus;
        #endregion

        #region BranchesRegion
        private List<BranchDTO> GetBranchesList(int currencyId, int cityId)
        {
            var branchList = GetBranchesAsync().Result.Where(i => i.CityId.Equals(cityId)).ToList();
            var quotationList = GetQuotationsAsync().Result.Where(i => i.CurrencyId.Equals(currencyId)).Select(i => i.BranchId).ToList();

            List<BranchDTO> result = new List<BranchDTO>();

            foreach (var branch in branchList)
            {
                if (quotationList.Contains(branch.Id)) result.Add(branch);
            }

            return result;
        }

        public BankViewModel GetBankFromBranch(int branchId, int currencyId)
        {
            var branch = GetBranchesAsync().Result.FirstOrDefault(i => i.Id.Equals(branchId));
            var quotation = GetQuotationsAsync().Result.FirstOrDefault(i=>i.CurrencyId.Equals(currencyId));
            var phones = GetPhonesAsync().Result.Where(i=>i.BranchId.Equals(branchId)).Select(s=>s.PhoneNum).ToList();
            return new BankViewModel(branch.Bank.NameRus, branch.Name, branch.Adr, quotation.Buy, quotation.Sale, phones);
        }

        public List<BestOffersModel> GetBranchesAndOffers(int currencyId, int bankId, int cityId, bool key)
        {
            var branches = GetBranchesList(currencyId, cityId).Where(i => i.BankId.Equals(bankId)).ToList();
            return GetQuotationByBranches(currencyId, branches, key);
        }

        public List<BranchDTO> GetBranchesList(IEnumerable<int> idList) => 
            GetBranchesAsync().Result.Where(i => idList.Contains(i.Id)).Distinct().ToList();

        public GeoLocationModel GetCoordinates(int branchId)
        {
            var branches = GetBranchesAsync().Result;
            var branch = branches.FirstOrDefault(i => i.Id.Equals(branchId));
            var x = float.Parse(branch?.Latitude.Replace('.',','));
            var y = float.Parse(branch?.Longitude.Replace('.',','));

            GeoLocationModel location = new GeoLocationModel(
                branch?.Adr, 
                x, 
                y
                );
            return location;
        }

        private IEnumerable<BranchDTO> GetBranchesInCity(int id) => GetBranchesAsync().Result.Where(i => i.CityId.Equals(id));

        #endregion

        #region QuotationsRegion

        private IEnumerable<QuotationDTO> GetQuotations(int cityId)
        {
            var branches = GetBranchesAsync().Result.Where(i => i.CityId.Equals(cityId)).ToList();
            return GetQuotationsAsync().Result.Where(i => branches.Select(j => j.Id).ToList().Contains(i.BranchId)).Distinct().ToList();
        }

        public List<BestOffersModel> GetBestOffer(int cityId, int currencyId, bool key)
        {
            var quotations = GetQuotations(cityId).Where(i=>i.CurrencyId.Equals(currencyId)).ToList();
            var bestOffers = key ? BestOfferSale(quotations) : BestOfferBuy(quotations);
            List<int> branchesId = bestOffers.Select(i => i.BranchId).ToList();
            var branches = GetBranchesList(branchesId);
            List<BestOffersModel> result = new List<BestOffersModel>();

            for (int i = 0; i < branches.Count(); i++)
            {
                result.Add(new BestOffersModel(
                    branches[i].BankId, 
                    branches[i].Bank.NameRus, 
                    branches[i].Id, 
                    branches[i].Adr, 
                    bestOffers[i].Offer));
            }

            return result;
        }

        public List<BestOffersModel> GetQuotationByBranches(int currencyId, List<BranchDTO> branches, bool key)
        {
            List<BestOffersModel> result = new List<BestOffersModel>();

            var quotations = GetQuotationsAsync().Result.Where(i => branches.Select(i => i.Id).Contains(i.BranchId)).ToList();
            var quotationsByCurrency = quotations.Where(i => i.CurrencyId.Equals(currencyId)).ToList();

            for (int i = 0; i < branches.Count(); i++)
            {
                var branch = branches[i];
                var quotation = quotationsByCurrency.FirstOrDefault(i => i.BranchId.Equals(branch.Id));
                string offer = key ? quotation.Sale : quotation.Buy;
                result.Add(new BestOffersModel(branch.BankId, branch.Bank.NameRus, branch.Id, branch.Adr, offer));
            }

            return result;
        }

        private List<BestOffers> BestOfferBuy(List<QuotationDTO> data)
        {
            var bestOffers = new List<BestOffers>();
            var best = data.OrderByDescending(i => i.Buy).Take(5);

            foreach (var b in best)
            {
                bestOffers.Add(new BestOffers(b.BranchId, b.Buy));
            }

            return bestOffers;
        }

        private List<BestOffers> BestOfferSale(List<QuotationDTO> data)
        {
            var bestOffers = new List<BestOffers>();
            var best = data.OrderBy(i => i.Sale).Take(5);

            foreach (var b in best)
            {
                bestOffers.Add(new BestOffers(b.BranchId, b.Sale));
            }

            return bestOffers;
        }
        #endregion

        #region CurrenciesRegion
        public string[] GetCurrenciesList(int cityId)
        {
            var branches = GetBranchesInCity(cityId).ToList();
            var branchID = branches.Select(i => i.Id).Distinct();
            var quotations = GetQuotationsAsync().Result.Where(i => branchID.Contains(i.BranchId)).ToList();

            var currencies = GetCurrenciesAsync().Result.Where(i => quotations.Select(i => i.CurrencyId).ToList().Contains(i.Id)).Distinct().ToArray();

            List<string> result = new List<string>();

            foreach (var c in currencies)
            {
                result.Add($"{c.Logo}  {c.NameRus}");
            }

            return result.ToArray();
        }
        #endregion

        #region UserStateService

        public UserStateDTO GetUserState(long userId) => 
            _userStateService.GetState(userId) ?? new UserStateDTO() { UserId = userId };

        public void SaveState(UserStateDTO userState)
        {
            var isExist = _userStateService.GetData().FirstOrDefault(i => i.UserId.Equals(userState.UserId));
            if (isExist != null) _userStateService.Update(userState);
            else _userStateService.Add(userState);
        }
        #endregion
    }
}
